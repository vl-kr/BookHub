using System.Text.Json.Serialization;
using BusinessLayer;
using BusinessLayer.Middlewares;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using WebAPI.Extensions;
using WebAPI.Middlewares;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.RegisterServices();
builder.Services.RegisterCoordinators();

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddConfiguredDbContext(configuration);

builder.Services.AddIdentity();
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddSwaggerGenWithJwt();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BookHubDbContext>();

    if (app.Environment.IsDevelopment())
    {
        //dbContext.Database.EnsureDeleted();
    }

    dbContext.Database.EnsureCreated();
    await services.SeedRoles();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>("[API]");
app.UseMiddleware<TimingMiddleware>();

//app.UseMiddleware<TokenAuthenticationMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TransformOutputMiddleware>();

app.MapControllers();

app.Run();

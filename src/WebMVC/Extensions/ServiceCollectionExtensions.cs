using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebMVC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddDefaultIdentity<LocalIdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<BookHubDbContext>();
        return services;
    }

    public static IServiceCollection AddConfiguredDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var databaseProvider = configuration.GetSection("DatabaseProvider").Value;
        services.AddDbContextFactory<BookHubDbContext>(options =>
        {
            switch (databaseProvider)
            {
                case "PostgreSQL":
                    options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
                    break;
                case "SQLite":
                    options.UseSqlite(configuration.GetConnectionString("SQLiteConnection"));
                    break;
                default:
                    throw new Exception("Invalid database provider");
            }
        });

        return services;
    }
}

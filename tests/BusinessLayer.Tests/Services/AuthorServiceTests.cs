using BusinessLayer.DTOs.Requests.Author;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.AuthorFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class AuthorServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public AuthorServiceTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddIdentity()
            .AddUnitOfWork()
            .AddAutoMapper()
            .AddRepositories()
            .AddServices()
            .AddMockedDbContext();
    }

    [Fact]
    public async Task CreateAuthor_ShouldReturnAuthorDTO()
    {
        // Arrange
        var authors = AuthorSeeder.PrepareAuthorModels();
        var author = authors.First();

        var authorRequest = new AuthorRequest { Name = author.Name };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.CreateAuthor(authorRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(author.Name, result.Data.Name);
    }

    [Fact]
    public async Task GetAuthors_ShouldReturnAuthorDTOs()
    {
        // Arrange
        var authors = AuthorSeeder.PrepareAuthorModels();
        var authorIds = authors.Select(a => a.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.GetAuthors(
            new PageOptions { PageSize = authors.Count },
            new AuthorFilter()
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(authors.Count, result.Data.Count());
        Assert.All(result.Data, authorSummary => Assert.Contains(authorSummary.Id, authorIds));
    }

    [Fact]
    public async Task GetAuthor_ShouldReturnAuthorDTO()
    {
        // Arrange
        var author = AuthorSeeder.PrepareAuthorModels().First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.GetAuthor(author.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(author.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetAuthor_ShouldReturnNotFound()
    {
        // Arrange
        var authorId = AuthorSeeder.PrepareAuthorModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.GetAuthor(authorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task UpdateAuthor_ShouldEditAuthorAndReturnDTO()
    {
        // Arrange
        var authors = AuthorSeeder.PrepareAuthorModels();
        var author = authors.First();

        var authorRequest = new AuthorRequest { Name = "updated name" };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.UpdateAuthor(author.Id, authorRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task DeleteAuthor_ShouldReturnNoContent()
    {
        // Arrange
        var authors = AuthorSeeder.PrepareAuthorModels();
        var author = authors.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.DeleteAuthor(author.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }
}

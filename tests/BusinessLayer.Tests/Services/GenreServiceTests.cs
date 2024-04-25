using BusinessLayer.DTOs.Requests.Genre;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.GenreFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class GenreServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public GenreServiceTests()
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
    public async Task CreateGenre_ShouldReturnGenreDTO()
    {
        // Arrange
        var genres = GenreSeeder.PrepareGenreModels();
        var genre = genres.First();

        var genreRequest = new GenreRequest { Name = genre.Name };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        // Act
        var result = await genreService.CreateGenre(genreRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(genre.Name, result.Data.Name);
    }

    [Fact]
    public async Task GetGenres_ShouldReturnGenreDTOs()
    {
        // Arrange
        var genres = GenreSeeder.PrepareGenreModels();
        var genreIds = genres.Select(a => a.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        // Act
        var result = await genreService.GetGenres(
            new PageOptions { PageSize = genres.Count },
            new GenreFilter()
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(genres.Count, result.Data.Count());
        Assert.All(result.Data, genreSummary => Assert.Contains(genreSummary.Id, genreIds));
    }

    [Fact]
    public async Task GetGenre_ShouldReturnGenreDTO()
    {
        // Arrange
        var genre = GenreSeeder.PrepareGenreModels().First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        // Act
        var result = await genreService.GetGenre(genre.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(genre.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetGenre_ShouldReturnNotFound()
    {
        // Arrange
        var genreId = GenreSeeder.PrepareGenreModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        // Act
        var result = await genreService.GetGenre(genreId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task UpdateGenre_ShouldEditGenreAndReturnDTO()
    {
        // Arrange
        var genres = GenreSeeder.PrepareGenreModels();
        var genre = genres.First();

        var genreRequest = new GenreRequest { Name = "updated name" };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        // Act
        var result = await genreService.UpdateGenre(genre.Id, genreRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task DeleteGenre_ShouldReturnNoContent()
    {
        // Arrange
        var genres = GenreSeeder.PrepareGenreModels();
        var genre = genres.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        // Act
        var result = await genreService.DeleteGenre(genre.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }
}

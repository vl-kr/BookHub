using BusinessLayer.DTOs.Requests.Wishlist;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class WishlistServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public WishlistServiceTests()
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
    public async Task GetWishlists_ShouldReturnWishlistsDTO()
    {
        // Arrange
        var wishlists = WishlistSeeder.PrepareWishlistModels();
        var wishlistIds = wishlists.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistService = scope.ServiceProvider.GetRequiredService<IWishlistService>();

        // Act
        var result = await wishlistService.GetWishlists(
            new PageOptions { PageSize = wishlists.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(wishlists.Count, result.Data.Count());
        Assert.All(result.Data, wishlist => Assert.Contains(wishlist.Id, wishlistIds));
    }

    [Fact]
    public async Task GetWishlist_ShouldReturnWishlistDTO()
    {
        // Arrange
        var wishlists = WishlistSeeder.PrepareWishlistModels();
        var wishlist = wishlists.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistService = scope.ServiceProvider.GetRequiredService<IWishlistService>();

        // Act
        var result = await wishlistService.GetWishlist(wishlist.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(wishlist.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetWishlist_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = WishlistSeeder.PrepareWishlistModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistService = scope.ServiceProvider.GetRequiredService<IWishlistService>();

        // Act
        var result = await wishlistService.GetWishlist(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteWishlist_ShouldReturnNoContent()
    {
        var wishlists = WishlistSeeder.PrepareWishlistModels();
        var wishlistId = wishlists.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistService = scope.ServiceProvider.GetRequiredService<IWishlistService>();

        // Act
        var result = await wishlistService.DeleteWishlist(wishlistId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateWishlist_ShouldReturnWishlistDTO()
    {
        // Arrange
        var wishlistRequest = new WishlistRequest { CustomerId = 4 };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistService = scope.ServiceProvider.GetRequiredService<IWishlistService>();

        // Act
        var result = await wishlistService.CreateWishlist(wishlistRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateWishlist_ShouldEditWishlistAndReturnDTO()
    {
        // Arrange
        var wishlistId = WishlistSeeder.PrepareWishlistModels().First().Id;
        var wishlistRequest = new WishlistRequest { CustomerId = 1 };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistService = scope.ServiceProvider.GetRequiredService<IWishlistService>();

        // Act
        var result = await wishlistService.UpdateWishlist(wishlistId, wishlistRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }
}

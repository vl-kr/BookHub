using BusinessLayer.DTOs.Requests.WishlistItem;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class WishlistItemServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public WishlistItemServiceTests()
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
    public async Task GetWishlistItems_ShouldReturnWishlistItemsDTO()
    {
        // Arrange
        var wishlistItems = WishlistItemSeeder.PrepareWishlistItemModels();
        var wishlistItemIds = wishlistItems.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistItemService = scope.ServiceProvider.GetRequiredService<IWishlistItemService>();

        // Act
        var result = await wishlistItemService.GetWishlistItems(
            new PageOptions { PageSize = wishlistItems.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(wishlistItems.Count, result.Data.Count());
        Assert.All(result.Data, wishlistItem => Assert.Contains(wishlistItem.Id, wishlistItemIds));
    }

    [Fact]
    public async Task GetWishlistItem_ShouldReturnWishlistItemDTO()
    {
        // Arrange
        var wishlistItems = WishlistItemSeeder.PrepareWishlistItemModels();
        var wishlistItem = wishlistItems.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistItemService = scope.ServiceProvider.GetRequiredService<IWishlistItemService>();

        // Act
        var result = await wishlistItemService.GetWishlistItem(wishlistItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(wishlistItem.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetWishlistItem_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = WishlistItemSeeder.PrepareWishlistItemModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistItemService = scope.ServiceProvider.GetRequiredService<IWishlistItemService>();

        // Act
        var result = await wishlistItemService.GetWishlistItem(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteWishlistItem_ShouldReturnNoContent()
    {
        var wishlistItems = WishlistItemSeeder.PrepareWishlistItemModels();
        var wishlistItemId = wishlistItems.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistItemService = scope.ServiceProvider.GetRequiredService<IWishlistItemService>();

        // Act
        var result = await wishlistItemService.DeleteWishlistItem(wishlistItemId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateWishlistItem_ShouldReturnWishlistItemDTO()
    {
        // Arrange
        var wishlistItemRequest = new WishlistItemRequest { WishlistId = 1, BookId = 1 };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistItemService = scope.ServiceProvider.GetRequiredService<IWishlistItemService>();

        // Act
        var result = await wishlistItemService.CreateWishlistItem(wishlistItemRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateWishlistItem_ShouldEditWishlistItemAndReturnDTO()
    {
        // Arrange
        var wishlistItemId = WishlistItemSeeder.PrepareWishlistItemModels().First().Id;
        var wishlistItemRequest = new WishlistItemRequest { WishlistId = 1, BookId = 1 };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var wishlistItemService = scope.ServiceProvider.GetRequiredService<IWishlistItemService>();

        // Act
        var result = await wishlistItemService.UpdateWishlistItem(
            wishlistItemId,
            wishlistItemRequest
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }
}

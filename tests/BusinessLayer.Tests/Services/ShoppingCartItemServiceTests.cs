using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class ShoppingCartItemServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public ShoppingCartItemServiceTests()
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
    public async Task GetShoppingCartItems_ShouldReturnShoppingCartItemsDTO()
    {
        // Arrange
        var shoppingCartItems = ShoppingCartItemSeeder.PrepareShoppingCartItemModels();
        var shoppingCartItemIds = shoppingCartItems.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartItemService =
            scope.ServiceProvider.GetRequiredService<IShoppingCartItemService>();

        // Act
        var result = await shoppingCartItemService.GetShoppingCartItems(
            new PageOptions { PageSize = shoppingCartItems.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shoppingCartItems.Count, result.Data.Count());
        Assert.All(
            result.Data,
            shoppingCartItem => Assert.Contains(shoppingCartItem.Id, shoppingCartItemIds)
        );
    }

    [Fact]
    public async Task GetShoppingCartItem_ShouldReturnShoppingCartItemDTO()
    {
        // Arrange
        var shoppingCartItems = ShoppingCartItemSeeder.PrepareShoppingCartItemModels();
        var shoppingCartItem = shoppingCartItems.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartItemService =
            scope.ServiceProvider.GetRequiredService<IShoppingCartItemService>();

        // Act
        var result = await shoppingCartItemService.GetShoppingCartItem(shoppingCartItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shoppingCartItem.Id, result.Data.Id);
        Assert.Equal(shoppingCartItem.Quantity, result.Data.Quantity);
    }

    [Fact]
    public async Task GetShoppingCartItem_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId =
            ShoppingCartItemSeeder.PrepareShoppingCartItemModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartItemService =
            scope.ServiceProvider.GetRequiredService<IShoppingCartItemService>();

        // Act
        var result = await shoppingCartItemService.GetShoppingCartItem(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteShoppingCartItem_ShouldReturnNoContent()
    {
        var shoppingCartItems = ShoppingCartItemSeeder.PrepareShoppingCartItemModels();
        var shoppingCartItemId = shoppingCartItems.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartItemService =
            scope.ServiceProvider.GetRequiredService<IShoppingCartItemService>();

        // Act
        var result = await shoppingCartItemService.DeleteShoppingCartItem(shoppingCartItemId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateShoppingCartItem_ShouldReturnShoppingCartItemDTO()
    {
        // Arrange
        var shoppingCartItemRequest = new ShoppingCartItemRequest
        {
            Quantity = 99,
            ShoppingCartId = 1,
            BookId = 1
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartItemService =
            scope.ServiceProvider.GetRequiredService<IShoppingCartItemService>();

        // Act
        var result = await shoppingCartItemService.CreateShoppingCartItem(shoppingCartItemRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateShoppingCartItem_ShouldEditShoppingCartItemAndReturnDTO()
    {
        // Arrange
        var shoppingCartItemId = ShoppingCartItemSeeder.PrepareShoppingCartItemModels().First().Id;
        var shoppingCartItemRequest = new ShoppingCartItemRequest
        {
            Quantity = 990,
            ShoppingCartId = 1,
            BookId = 1
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartItemService =
            scope.ServiceProvider.GetRequiredService<IShoppingCartItemService>();

        // Act
        var result = await shoppingCartItemService.UpdateShoppingCartItem(
            shoppingCartItemId,
            shoppingCartItemRequest
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }
}

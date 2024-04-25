using BusinessLayer.DTOs.Requests.ShoppingCart;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class ShoppingCartServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public ShoppingCartServiceTests()
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
    public async Task GetShoppingCarts_ShouldReturnShoppingCartsDTO()
    {
        // Arrange
        var shoppingCarts = ShoppingCartSeeder.PrepareShoppingCartModels();
        var shoppingCartIds = shoppingCarts.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartService = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

        // Act
        var result = await shoppingCartService.GetShoppingCarts(
            new PageOptions { PageSize = shoppingCarts.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shoppingCarts.Count, result.Data.Count());
        Assert.All(result.Data, shoppingCart => Assert.Contains(shoppingCart.Id, shoppingCartIds));
    }

    [Fact]
    public async Task GetShoppingCart_ShouldReturnShoppingCartDTO()
    {
        // Arrange
        var shoppingCarts = ShoppingCartSeeder.PrepareShoppingCartModels();
        var shoppingCart = shoppingCarts.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartService = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

        // Act
        var result = await shoppingCartService.GetShoppingCart(shoppingCart.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shoppingCart.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetShoppingCart_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = ShoppingCartSeeder.PrepareShoppingCartModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartService = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

        // Act
        var result = await shoppingCartService.GetShoppingCart(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteShoppingCart_ShouldReturnNoContent()
    {
        var shoppingCarts = ShoppingCartSeeder.PrepareShoppingCartModels();
        var shoppingCartId = shoppingCarts.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartService = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

        // Act
        var result = await shoppingCartService.DeleteShoppingCart(shoppingCartId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateShoppingCart_ShouldReturnShoppingCartDTO()
    {
        // Arrange
        var shoppingCartRequest = new ShoppingCartRequest { CustomerId = 4 };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartService = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

        // Act
        var result = await shoppingCartService.CreateShoppingCart(shoppingCartRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateShoppingCart_ShouldEditShoppingCartAndReturnDTO()
    {
        // Arrange
        var shoppingCartId = ShoppingCartSeeder.PrepareShoppingCartModels().First().Id;
        var shoppingCartRequest = new ShoppingCartRequest { CustomerId = 1 };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shoppingCartService = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

        // Act
        var result = await shoppingCartService.UpdateShoppingCart(
            shoppingCartId,
            shoppingCartRequest
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        //Assert.Empty(result.Data.ShoppingCartItems);
    }
}

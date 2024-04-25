using BusinessLayer.DTOs.Requests.OrderItem;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class OrderItemServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public OrderItemServiceTests()
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
    public async Task GetOrderItems_ShouldReturnOrderItemsDTO()
    {
        // Arrange
        var orderItems = OrderItemSeeder.PrepareOrderItemModels();
        var orderItemIds = orderItems.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();

        // Act
        var result = await orderItemService.GetOrderItems(
            new PageOptions { PageSize = orderItems.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orderItems.Count, result.Data.Count());
        Assert.All(result.Data, orderItem => Assert.Contains(orderItem.Id, orderItemIds));
    }

    [Fact]
    public async Task GetOrderItem_ShouldReturnOrderItemDTO()
    {
        // Arrange
        var orderItems = OrderItemSeeder.PrepareOrderItemModels();
        var orderItem = orderItems.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();

        // Act
        var result = await orderItemService.GetOrderItem(orderItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orderItem.Id, result.Data.Id);
        Assert.Equal(orderItem.Quantity, result.Data.Quantity);
    }

    [Fact]
    public async Task GetOrderItem_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = OrderItemSeeder.PrepareOrderItemModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();

        // Act
        var result = await orderItemService.GetOrderItem(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteOrderItem_ShouldReturnNoContent()
    {
        var orderItems = OrderItemSeeder.PrepareOrderItemModels();
        var orderItemId = orderItems.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();

        // Act
        var result = await orderItemService.DeleteOrderItem(orderItemId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateOrderItem_ShouldReturnOrderItemDTO()
    {
        // Arrange
        var orderItemRequest = new OrderItemRequest
        {
            Quantity = 99,
            OrderId = 1,
            BookId = 1
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();

        // Act
        var result = await orderItemService.CreateOrderItem(orderItemRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateOrderItem_ShouldEditOrderItemAndReturnDTO()
    {
        // Arrange
        var orderItemId = OrderItemSeeder.PrepareOrderItemModels().First().Id;
        var orderItemRequest = new OrderItemRequest
        {
            Quantity = 990,
            OrderId = 1,
            BookId = 1
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderItemService = scope.ServiceProvider.GetRequiredService<IOrderItemService>();

        // Act
        var result = await orderItemService.UpdateOrderItem(orderItemId, orderItemRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }
}

using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.OrderFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class OrderServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public OrderServiceTests()
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
    public async Task GetOrders_ShouldReturnOrdersDTO()
    {
        // Arrange
        var orders = OrderSeeder.PrepareOrderModels();
        var orderIds = orders.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        // Act
        var result = await orderService.GetOrders(
            new PageOptions { PageSize = orders.Count },
            new OrderFilter()
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orders.Count, result.Data.Count());
        Assert.All(result.Data, order => Assert.Contains(order.Id, orderIds));
    }

    [Fact]
    public async Task GetOrder_ShouldReturnOrderDTO()
    {
        // Arrange
        var orders = OrderSeeder.PrepareOrderModels();
        var order = orders.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        // Act
        var result = await orderService.GetOrder(order.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(order.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetOrder_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = OrderSeeder.PrepareOrderModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        // Act
        var result = await orderService.GetOrder(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnNoContent()
    {
        var orders = OrderSeeder.PrepareOrderModels();
        var orderId = orders.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        // Act
        var result = await orderService.DeleteOrder(orderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnOrderDTO()
    {
        // Arrange
        var orderRequest = new OrderRequest
        {
            OrderStatusId = 1,
            ShippingAddressId = 1,
            BillingAddressId = 1,
            CustomerId = 1,
            OrderItemIds = new List<int> { 1 }
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        // Act
        var result = await orderService.CreateOrder(orderRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orderRequest.OrderStatusId, result.Data.Status?.Id);
    }

    [Fact]
    public async Task UpdateOrder_ShouldEditOrderAndReturnDTO()
    {
        // Arrange
        var orderId = OrderSeeder.PrepareOrderModels().First().Id;
        var orderEditDto = new OrderEditRequest
        {
            OrderStatusId = 1,
            ShippingAddressId = 1,
            BillingAddressId = 1
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        // Act
        var result = await orderService.UpdateOrder(orderId, orderEditDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orderEditDto.BillingAddressId, result.Data.BillingAddress?.Id);
    }
}

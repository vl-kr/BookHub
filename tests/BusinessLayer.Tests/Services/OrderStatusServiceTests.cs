using BusinessLayer.DTOs.Requests.OrderStatus;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class OrderStatusServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public OrderStatusServiceTests()
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
    public async Task GetOrderStatus_ShouldReturnOrderStatussDTO()
    {
        // Arrange
        var orderStatuss = OrderStatusSeeder.PrepareOrderStatusModels();
        var orderStatusIds = orderStatuss.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderStatusService = scope.ServiceProvider.GetRequiredService<IOrderStatusService>();

        // Act
        var result = await orderStatusService.GetOrderStatuses(
            new PageOptions { PageSize = orderStatuss.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orderStatuss.Count, result.Data.Count());
        Assert.All(result.Data, orderStatus => Assert.Contains(orderStatus.Id, orderStatusIds));
    }

    [Fact]
    public async Task GetOrderStatus_ShouldReturnOrderStatusDTO()
    {
        // Arrange
        var orderStatuss = OrderStatusSeeder.PrepareOrderStatusModels();
        var orderStatus = orderStatuss.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderStatusService = scope.ServiceProvider.GetRequiredService<IOrderStatusService>();

        // Act
        var result = await orderStatusService.GetOrderStatus(orderStatus.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orderStatus.Id, result.Data.Id);
        Assert.Equal(orderStatus.Name, result.Data.Name);
    }

    [Fact]
    public async Task GetOrderStatus_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = OrderStatusSeeder.PrepareOrderStatusModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderStatusService = scope.ServiceProvider.GetRequiredService<IOrderStatusService>();

        // Act
        var result = await orderStatusService.GetOrderStatus(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteOrderStatus_ShouldReturnNoContent()
    {
        var orderStatuss = OrderStatusSeeder.PrepareOrderStatusModels();
        var orderStatusId = orderStatuss.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderStatusService = scope.ServiceProvider.GetRequiredService<IOrderStatusService>();

        // Act
        var result = await orderStatusService.DeleteOrderStatus(orderStatusId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateOrderStatus_ShouldReturnOrderStatusDTO()
    {
        // Arrange
        var orderStatusRequest = new OrderStatusRequest { Name = "new name" };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderStatusService = scope.ServiceProvider.GetRequiredService<IOrderStatusService>();

        // Act
        var result = await orderStatusService.CreateOrderStatus(orderStatusRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(orderStatusRequest.Name, result.Data.Name);
    }

    [Fact]
    public async Task UpdateOrderStatus_ShouldEditOrderStatusAndReturnDTO()
    {
        // Arrange
        var orderStatusId = OrderStatusSeeder.PrepareOrderStatusModels().First().Id;
        var orderStatusRequest = new OrderStatusRequest { Name = "new name" };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var orderStatusService = scope.ServiceProvider.GetRequiredService<IOrderStatusService>();

        // Act
        var result = await orderStatusService.UpdateOrderStatus(orderStatusId, orderStatusRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }
}

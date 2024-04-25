using BusinessLayer.DTOs.Requests.ShippingMethod;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class ShippingMethodServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public ShippingMethodServiceTests()
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
    public async Task GetShippingMethod_ShouldReturnShippingMethodsDTO()
    {
        // Arrange
        var shippingMethods = ShippingMethodSeeder.PrepareShippingMethodModels();
        var shippingMethodIds = shippingMethods.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shippingMethodService =
            scope.ServiceProvider.GetRequiredService<IShippingMethodService>();

        // Act
        var result = await shippingMethodService.GetShippingMethods(
            new PageOptions { PageSize = shippingMethods.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shippingMethods.Count, result.Data.Count());
        Assert.All(
            result.Data,
            shippingMethod => Assert.Contains(shippingMethod.Id, shippingMethodIds)
        );
    }

    [Fact]
    public async Task GetShippingMethod_ShouldReturnShippingMethodDTO()
    {
        // Arrange
        var shippingMethods = ShippingMethodSeeder.PrepareShippingMethodModels();
        var shippingMethod = shippingMethods.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shippingMethodService =
            scope.ServiceProvider.GetRequiredService<IShippingMethodService>();

        // Act
        var result = await shippingMethodService.GetShippingMethod(shippingMethod.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shippingMethod.Id, result.Data.Id);
        Assert.Equal(shippingMethod.Name, result.Data.Name);
    }

    [Fact]
    public async Task GetShippingMethod_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = ShippingMethodSeeder.PrepareShippingMethodModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shippingMethodService =
            scope.ServiceProvider.GetRequiredService<IShippingMethodService>();

        // Act
        var result = await shippingMethodService.GetShippingMethod(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteShippingMethod_ShouldReturnNoContent()
    {
        var shippingMethods = ShippingMethodSeeder.PrepareShippingMethodModels();
        var shippingMethodId = shippingMethods.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shippingMethodService =
            scope.ServiceProvider.GetRequiredService<IShippingMethodService>();

        // Act
        var result = await shippingMethodService.DeleteShippingMethod(shippingMethodId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateShippingMethod_ShouldReturnShippingMethodDTO()
    {
        // Arrange
        var shippingMethodRequest = new ShippingMethodRequest
        {
            Name = "new name",
            Description = "new description"
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shippingMethodService =
            scope.ServiceProvider.GetRequiredService<IShippingMethodService>();

        // Act
        var result = await shippingMethodService.CreateShippingMethod(shippingMethodRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shippingMethodRequest.Name, result.Data.Name);
    }

    [Fact]
    public async Task UpdateShippingMethod_ShouldEditShippingMethodAndReturnDTO()
    {
        // Arrange
        var shippingMethodId = ShippingMethodSeeder.PrepareShippingMethodModels().First().Id;
        var shippingMethodRequest = new ShippingMethodRequest
        {
            Name = "updated name",
            Description = "updated description"
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var shippingMethodService =
            scope.ServiceProvider.GetRequiredService<IShippingMethodService>();

        // Act
        var result = await shippingMethodService.UpdateShippingMethod(
            shippingMethodId,
            shippingMethodRequest
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(shippingMethodRequest.Description, result.Data.Description);
    }
}

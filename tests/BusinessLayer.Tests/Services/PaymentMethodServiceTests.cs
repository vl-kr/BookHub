using BusinessLayer.DTOs.Requests.PaymentMethod;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class PaymentMethodServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public PaymentMethodServiceTests()
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
    public async Task GetPaymentMethod_ShouldReturnPaymentMethodsDTO()
    {
        // Arrange
        var paymentMethods = PaymentMethodSeeder.PreparePaymentMethodModels();
        var paymentMethodIds = paymentMethods.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var paymentMethodService =
            scope.ServiceProvider.GetRequiredService<IPaymentMethodService>();

        // Act
        var result = await paymentMethodService.GetPaymentMethods(
            new PageOptions { PageSize = paymentMethods.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(paymentMethods.Count, result.Data.Count());
        Assert.All(
            result.Data,
            paymentMethod => Assert.Contains(paymentMethod.Id, paymentMethodIds)
        );
    }

    [Fact]
    public async Task GetPaymentMethod_ShouldReturnPaymentMethodDTO()
    {
        // Arrange
        var paymentMethods = PaymentMethodSeeder.PreparePaymentMethodModels();
        var paymentMethod = paymentMethods.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var paymentMethodService =
            scope.ServiceProvider.GetRequiredService<IPaymentMethodService>();

        // Act
        var result = await paymentMethodService.GetPaymentMethod(paymentMethod.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(paymentMethod.Id, result.Data.Id);
        Assert.Equal(paymentMethod.Name, result.Data.Name);
    }

    [Fact]
    public async Task GetPaymentMethod_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = PaymentMethodSeeder.PreparePaymentMethodModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var paymentMethodService =
            scope.ServiceProvider.GetRequiredService<IPaymentMethodService>();

        // Act
        var result = await paymentMethodService.GetPaymentMethod(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeletePaymentMethod_ShouldReturnNoContent()
    {
        var paymentMethods = PaymentMethodSeeder.PreparePaymentMethodModels();
        var paymentMethodId = paymentMethods.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var paymentMethodService =
            scope.ServiceProvider.GetRequiredService<IPaymentMethodService>();

        // Act
        var result = await paymentMethodService.DeletePaymentMethod(paymentMethodId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreatePaymentMethod_ShouldReturnPaymentMethodDTO()
    {
        // Arrange
        var paymentMethodRequest = new PaymentMethodRequest
        {
            Name = "new name",
            Description = "new description"
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var paymentMethodService =
            scope.ServiceProvider.GetRequiredService<IPaymentMethodService>();

        // Act
        var result = await paymentMethodService.CreatePaymentMethod(paymentMethodRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(paymentMethodRequest.Name, result.Data.Name);
    }

    [Fact]
    public async Task UpdatePaymentMethod_ShouldEditPaymentMethodAndReturnDTO()
    {
        // Arrange
        var paymentMethodId = PaymentMethodSeeder.PreparePaymentMethodModels().First().Id;
        var paymentMethodRequest = new PaymentMethodRequest
        {
            Name = "updated name",
            Description = "updated description"
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var paymentMethodService =
            scope.ServiceProvider.GetRequiredService<IPaymentMethodService>();

        // Act
        var result = await paymentMethodService.UpdatePaymentMethod(
            paymentMethodId,
            paymentMethodRequest
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(paymentMethodRequest.Description, result.Data.Description);
    }
}

using BusinessLayer.DTOs.Requests.Customer;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class CustomerServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public CustomerServiceTests()
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
    public async Task CreateCustomer_ShouldReturnConflict()
    {
        // Arrange
        var customers = CustomerSeeder.PrepareCustomerModels();
        var customer = customers.First();

        var customerRequest = new CustomerRequest { FirstName = customer.FirstName, UserId = 1 };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        // Act
        var result = await customerService.CreateCustomer(customerRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task GetCustomers_ShouldReturnCustomerDTOs()
    {
        // Arrange
        var customers = CustomerSeeder.PrepareCustomerModels();
        var customerIds = customers.Select(a => a.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        // Act
        var result = await customerService.GetCustomers(
            new PageOptions { PageSize = customers.Count }
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(customers.Count, result.Data.Count());
        Assert.All(
            result.Data,
            customerSummary => Assert.Contains(customerSummary.Id, customerIds)
        );
    }

    [Fact]
    public async Task GetCustomer_ShouldReturnCustomerDTO()
    {
        // Arrange
        var customer = CustomerSeeder.PrepareCustomerModels().First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        // Act
        var result = await customerService.GetCustomer(customer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(customer.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetCustomer_ShouldReturnNotFound()
    {
        // Arrange
        var customerId = CustomerSeeder.PrepareCustomerModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        // Act
        var result = await customerService.GetCustomer(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldEditCustomerAndReturnDTO()
    {
        // Arrange
        var customers = CustomerSeeder.PrepareCustomerModels();
        var customer = customers.First();

        var customerRequest = new CustomerRequest { FirstName = "updated name" };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        // Act
        var result = await customerService.UpdateCustomer(customer.Id, customerRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnNoContent()
    {
        // Arrange
        var customers = CustomerSeeder.PrepareCustomerModels();
        var customer = customers.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

        // Act
        var result = await customerService.DeleteCustomer(customer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }
}

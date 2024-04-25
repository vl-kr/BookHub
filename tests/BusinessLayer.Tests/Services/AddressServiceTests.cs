using BusinessLayer.DTOs.Requests.Address;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.AddressFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class AddressServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public AddressServiceTests()
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
    public async Task CreateAddress_ShouldReturnAddressDTO()
    {
        // Arrange
        var addresses = AddressSeeder.PrepareAddressModels();
        var address = addresses.First();

        var addressRequest = new AddressRequest { Street = address.Street, City = address.City };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();

        // Act
        var result = await addressService.CreateAddress(addressRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(address.Street, result.Data.Street);
        Assert.Equal(address.City, result.Data.City);
    }

    [Fact]
    public async Task GetAddresses_ShouldReturnAddressDTOs()
    {
        // Arrange
        var addresses = AddressSeeder.PrepareAddressModels();
        var addressId = addresses.Select(a => a.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();

        // Act
        var result = await addressService.GetAddresses(
            new PageOptions { PageSize = addresses.Count },
            new AddressFilter()
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(addresses.Count, result.Data.Count());
        Assert.All(result.Data, addressSummary => Assert.Contains(addressSummary.Id, addressId));
    }

    [Fact]
    public async Task GetAddress_ShouldReturnAddressDTO()
    {
        // Arrange
        var address = AddressSeeder.PrepareAddressModels().First();
        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();

        // Act
        var result = await addressService.GetAddress(address.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(address.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetAddress_ShouldReturnNotFound()
    {
        // Arrange
        var addressId = AddressSeeder.PrepareAddressModels().Max(x => x.Id) + 1;
        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();

        // Act
        var result = await addressService.GetAddress(addressId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task UpdateAddress_ShouldEditAddressAndReturnDTO()
    {
        // Arrange
        var addresses = AddressSeeder.PrepareAddressModels();
        var address = addresses.First();

        var addressRequest = new AddressRequest
        {
            Street = "updated street",
            City = "updated city"
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();

        // Act
        var result = await addressService.UpdateAddress(address.Id, addressRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task DeleteAddress_ShouldReturnNoContent()
    {
        // Arrange
        var addresses = AddressSeeder.PrepareAddressModels();
        var address = addresses.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();

        // Act
        var result = await addressService.DeleteAddress(address.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }
}

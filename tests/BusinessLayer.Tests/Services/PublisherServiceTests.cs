using BusinessLayer.DTOs.Requests.Publisher;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.PublisherFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class PublisherServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public PublisherServiceTests()
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
    public async Task CreatePublisher_ShouldReturnPublisherDTO()
    {
        // Arrange
        var publishers = PublisherSeeder.PreparePublisherModels();
        var publisher = publishers.First();

        var publisherRequest = new PublisherRequest { Name = publisher.Name };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

        // Act
        var result = await publisherService.CreatePublisher(publisherRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(publisher.Name, result.Data.Name);
    }

    [Fact]
    public async Task GetPublishers_ShouldReturnPublisherDTOs()
    {
        // Arrange
        var publishers = PublisherSeeder.PreparePublisherModels();
        var publisherIds = publishers.Select(a => a.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

        // Act
        var result = await publisherService.GetPublishers(
            new PageOptions { PageSize = publishers.Count },
            new PublisherFilter()
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(publishers.Count, result.Data.Count());
        Assert.All(
            result.Data,
            publisherSummary => Assert.Contains(publisherSummary.Id, publisherIds)
        );
    }

    [Fact]
    public async Task GetPublisher_ShouldReturnPublisherDTO()
    {
        // Arrange
        var publisher = PublisherSeeder.PreparePublisherModels().First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

        // Act
        var result = await publisherService.GetPublisher(publisher.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(publisher.Id, result.Data.Id);
    }

    [Fact]
    public async Task GetPublisher_ShouldReturnNotFound()
    {
        // Arrange
        var publisherId = PublisherSeeder.PreparePublisherModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

        // Act
        var result = await publisherService.GetPublisher(publisherId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task UpdatePublisher_ShouldEditPublisherAndReturnDTO()
    {
        // Arrange
        var publishers = PublisherSeeder.PreparePublisherModels();
        var publisher = publishers.First();

        var publisherRequest = new PublisherRequest { Name = "updated name" };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

        // Act
        var result = await publisherService.UpdatePublisher(publisher.Id, publisherRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task DeletePublisher_ShouldReturnNoContent()
    {
        // Arrange
        var publishers = PublisherSeeder.PreparePublisherModels();
        var publisher = publishers.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

        // Act
        var result = await publisherService.DeletePublisher(publisher.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }
}

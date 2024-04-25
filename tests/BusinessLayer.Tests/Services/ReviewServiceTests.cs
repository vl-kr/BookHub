using BusinessLayer.DTOs.Requests.Review;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.ReviewFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class ReviewServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public ReviewServiceTests()
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
    public async Task GetReviews_ShouldReturnReviewsDTO()
    {
        // Arrange
        var reviews = ReviewSeeder.PrepareReviewModels();
        var reviewIds = reviews.Select(r => r.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();

        // Act
        var result = await reviewService.GetReviews(
            new PageOptions { PageSize = reviews.Count },
            new ReviewFilter()
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(reviews.Count, result.Data.Count());
        Assert.All(result.Data, review => Assert.Contains(review.Id, reviewIds));
    }

    [Fact]
    public async Task GetReview_ShouldReturnReviewDTO()
    {
        // Arrange
        var reviews = ReviewSeeder.PrepareReviewModels();
        var review = reviews.First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();

        // Act
        var result = await reviewService.GetReview(review.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(review.Id, result.Data.Id);
        Assert.Equal(review.Rating, result.Data.Rating);
    }

    [Fact]
    public async Task GetReview_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = ReviewSeeder.PrepareReviewModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();

        // Act
        var result = await reviewService.GetReview(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteReview_ShouldReturnNoContent()
    {
        var reviews = ReviewSeeder.PrepareReviewModels();
        var reviewId = reviews.First().Id;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();

        // Act
        var result = await reviewService.DeleteReview(reviewId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateReview_ShouldReturnReviewDTO()
    {
        // Arrange
        var reviewRequest = new ReviewRequest
        {
            Rating = 4,
            TextReview = "OK",
            BookId = 1,
            CustomerId = 1
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();

        // Act
        var result = await reviewService.CreateReview(reviewRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(reviewRequest.Rating, result.Data.Rating);
    }

    [Fact]
    public async Task UpdateReview_ShouldEditReviewAndReturnDTO()
    {
        var reviewId = ReviewSeeder.PrepareReviewModels().First().Id;
        // Arrange
        var reviewRequest = new ReviewRequest
        {
            Rating = 4,
            TextReview = "Updated Review",
            BookId = 1,
            CustomerId = 1
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var reviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();

        // Act
        var result = await reviewService.UpdateReview(reviewId, reviewRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(reviewRequest.TextReview, result.Data.TextReview);
    }
}

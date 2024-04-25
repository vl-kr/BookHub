using BusinessLayer.DTOs.Requests.Book;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.BookFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.FakeSeeding;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class BookServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public BookServiceTests()
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
    public async Task GetBooks_ShouldReturnBooksDTO()
    {
        // Arrange
        var books = BookSeeder.PrepareBookModels();
        var bookIds = books.Select(b => b.Id).ToArray();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.GetBooks(
            new PageOptions { PageSize = books.Count },
            new BookFilter()
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(books.Count, result.Data.Count());
        Assert.All(result.Data, book => Assert.Contains(book.Id, bookIds));
    }

    [Fact]
    public async Task GetBook_ShouldReturnBookDTO()
    {
        // Arrange
        var books = BookSeeder.PrepareBookModels();
        var bookId = books.Select(b => b.Id).First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.GetBook(bookId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(bookId, result.Data.Id);
    }

    [Fact]
    public async Task GetBook_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = BookSeeder.PrepareBookModels().Max(x => x.Id) + 1;

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.GetBook(nonExistentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteBook_ShouldReturnNoContent()
    {
        var books = BookSeeder.PrepareBookModels();
        var bookId = books.Select(b => b.Id).First();

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.DeleteBook(bookId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CreateBook_ShouldReturnBookDTO()
    {
        // Arrange
        var bookRequest = new BookRequest
        {
            Title = "Dune",
            Description = "A lot of sand",
            Price = 19.99m,
            ISBN = "1234567890",
            YearPublished = 1960,
            ImageUrl = "http://example.com/image.jpg",
            PublisherId = 1,
            PrimaryGenreId = 1,
            AuthorIds = new List<int> { 1, 2 },
            GenreIds = new List<int> { 1 }
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.CreateBook(bookRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Created, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(bookRequest.Description, result.Data.Description);
    }

    [Fact]
    public async Task CreateBook_ShouldReturnConflictMissingIds()
    {
        // Arrange
        var bookRequest = new BookRequest
        {
            Title = "Dune",
            Description = "A lot of sand",
            Price = 19.99m,
            ISBN = "1234567890",
            YearPublished = 1960,
            ImageUrl = "http://example.com/image.jpg",
            PublisherId = 1,
            PrimaryGenreId = 1,
            AuthorIds = new List<int>(), // no authors
            GenreIds = new List<int> { 1 }
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.CreateBook(bookRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task CreateBook_ShouldReturnConflictInvalidId()
    {
        // Arrange
        var bookRequest = new BookRequest
        {
            Title = "Dune",
            Description = "A lot of sand",
            Price = 19.99m,
            ISBN = "1234567890",
            YearPublished = 1960,
            ImageUrl = "http://example.com/image.jpg",
            PublisherId = -999, // invalid publisher ID
            PrimaryGenreId = 1,
            AuthorIds = new List<int> { 1, 2 },
            GenreIds = new List<int> { 1 }
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.CreateBook(bookRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task UpdateBook_ShouldEditBookAndReturnDTO()
    {
        // Arrange
        var books = BookSeeder.PrepareBookModels();
        var bookId = books.Select(b => b.Id).First();

        var bookRequest = new BookRequest
        {
            Title = "Updated Title",
            Description = "Updated Description",
            Price = 9.99m,
            ISBN = "1234567890",
            YearPublished = 20020,
            ImageUrl = "http://example.com/image.jpg",
            PublisherId = 1,
            PrimaryGenreId = 1,
            AuthorIds = new List<int> { 1, 2 },
            GenreIds = new List<int> { 1 }
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.UpdateBook(bookId, bookRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.OK, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(bookRequest.Title, result.Data.Title);
    }

    [Fact]
    public async Task UpdateBook_ShouldReturnConflictInvalidId()
    {
        // Arrange
        var books = BookSeeder.PrepareBookModels();
        var bookOriginal = books.First();

        var bookRequest = new BookRequest
        {
            Title = "Updated Title",
            Description = "Updated Description",
            Price = 9.99m,
            ISBN = "1234567890",
            YearPublished = 20020,
            ImageUrl = "http://example.com/image.jpg",
            PublisherId = -999, // invalid publisher ID
            PrimaryGenreId = 1,
            AuthorIds = new List<int> { 1, 2 },
            GenreIds = new List<int> { 1 }
        };

        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var mockedContext = MockedDbContext.CreateFromOptions(options);

        var serviceProvider = _serviceProviderBuilder.AddScoped(mockedContext).Create();

        using var scope = serviceProvider.CreateScope();
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.UpdateBook(bookOriginal.Id, bookRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ServiceResultCode.Conflict, result.StatusCode);
    }
}

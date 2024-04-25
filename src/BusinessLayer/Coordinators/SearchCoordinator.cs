using BusinessLayer.Coordinators.Interfaces;
using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.DTOs.Responses.Publisher;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.AuthorFilters;
using BusinessLayer.Services.Filtering.BookFilters;
using BusinessLayer.Services.Filtering.GenreFilters;
using BusinessLayer.Services.Filtering.PublisherFilters;
using BusinessLayer.Services.Interfaces;

namespace BusinessLayer.Coordinators;

public class SearchCoordinator : ISearchCoordinator
{
    private readonly IAuthorService _authorService;
    private readonly IBookService _bookService;
    private readonly IGenreService _genreService;
    private readonly IPublisherService _publisherService;

    public SearchCoordinator(
        IBookService bookService,
        IGenreService genreService,
        IAuthorService authorService,
        IPublisherService publisherService
    )
    {
        _bookService = bookService;
        _genreService = genreService;
        _authorService = authorService;
        _publisherService = publisherService;
    }

    public async Task<SearchResult> Search(string searchTerm)
    {
        var bookResult = await _bookService.GetBooks(
            new PageOptions { SearchTerm = searchTerm },
            new BookFilter()
        );
        var genreResult = await _genreService.GetGenres(
            new PageOptions { SearchTerm = searchTerm },
            new GenreFilter()
        );
        var authorResult = await _authorService.GetAuthors(
            new PageOptions { SearchTerm = searchTerm },
            new AuthorFilter()
        );
        var publisherResult = await _publisherService.GetPublishers(
            new PageOptions { SearchTerm = searchTerm },
            new PublisherFilter()
        );

        var bookResultData = bookResult is { StatusCode: ServiceResultCode.OK, Data: not null }
            ? bookResult.Data
            : new List<BookResponse>();
        var genreResultData = genreResult is { StatusCode: ServiceResultCode.OK, Data: not null }
            ? genreResult.Data
            : new List<GenreResponse>();
        var authorResultData = authorResult is { StatusCode: ServiceResultCode.OK, Data: not null }
            ? authorResult.Data
            : new List<AuthorResponse>();
        var publisherResultData = publisherResult
            is { StatusCode: ServiceResultCode.OK, Data: not null }
            ? publisherResult.Data
            : new List<PublisherResponse>();

        return new SearchResult
        {
            Books = bookResultData,
            Genres = genreResultData,
            Authors = authorResultData,
            Publishers = publisherResultData
        };
    }
}

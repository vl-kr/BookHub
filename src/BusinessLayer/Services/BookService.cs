using AutoMapper;
using BusinessLayer.DTOs.Requests.Book;
using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.BookFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class BookService : IBookService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public BookService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<BookResponse>> CreateBook(BookRequest bookRequest)
    {
        var book = _mapper.Map<Book>(bookRequest);

        var (isMappingSuccessful, errorMessage) = await MapRelatedEntitiesFromIds(
            book,
            bookRequest
        );
        if (!isMappingSuccessful)
            return new ServiceResult<BookResponse>(errorMessage, ServiceResultCode.Conflict);

        await _uow.BookRepository.AddAsync(book);
        await _uow.CommitAsync();
        return new ServiceResult<BookResponse>(
            _mapper.Map<BookResponse>(book),
            ServiceResultCode.Created
        );
    }

    public async Task<PaginationObject<Book>> GetPaginatedBooks(
        PageOptions pageOptions,
        BookFilter bookFilter
    )
    {
        var query = new EFCoreQueryObject<Book>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(bookFilter);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);

        var result = await query.GetPagedResultAsync(
            pageOptions.Page ?? 1,
            pageOptions.PageSize ?? 10
        );
        return result;
    }

    public async Task<ServiceResult<IEnumerable<BookResponse>>> GetBooks(
        PageOptions pageOptions,
        BookFilter bookFilter
    )
    {
        var query = new EFCoreQueryObject<Book>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(bookFilter);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var books = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<BookResponse>>(
            books.Select(_mapper.Map<BookResponse>)
        );
    }

    public async Task<ServiceResult<BookResponse>> GetBook(int id)
    {
        var book = await _uow.BookRepository.FindByIdWithAllRelatedDataAsync(id);
        if (book == null)
            return new ServiceResult<BookResponse>("Book not found", ServiceResultCode.NotFound);
        return new ServiceResult<BookResponse>(_mapper.Map<BookResponse>(book));
    }

    public async Task<ServiceResult<BookResponse>> UpdateBook(int id, BookRequest bookRequest)
    {
        var existingBook = await _uow.BookRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingBook == null)
            return new ServiceResult<BookResponse>("Book not found", ServiceResultCode.NotFound);

        _uow.BookRepository.Update(_mapper.Map(bookRequest, existingBook));

        var (isMappingSuccessful, errorMessage) = await MapRelatedEntitiesFromIds(
            existingBook,
            bookRequest
        );
        if (!isMappingSuccessful)
        {
            _uow.BookRepository.DiscardChanges(existingBook);
            await _uow.CommitAsync();
            return new ServiceResult<BookResponse>(errorMessage, ServiceResultCode.Conflict);
        }

        await _uow.CommitAsync();
        return new ServiceResult<BookResponse>(_mapper.Map<BookResponse>(existingBook));
    }

    public async Task<ServiceResult<BookResponse>> DeleteBook(int id)
    {
        var book = await _uow.BookRepository.FindByIdWithAllRelatedDataAsync(id);
        if (book == null)
            return new ServiceResult<BookResponse>("Book not found", ServiceResultCode.NotFound);

        try
        {
            _uow.BookRepository.Remove(book);
            await _uow.CommitAsync();
            return new ServiceResult<BookResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<BookResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    private async Task<(bool Success, string ErrorMessage)> MapRelatedEntitiesFromIds(
        Book book,
        BookRequest bookRequest
    )
    {
        var genres = await _uow.GenreRepository.FilterAsync(g =>
            bookRequest.GenreIds.Contains(g.Id)
        );

        var authors = await _uow.AuthorRepository.FilterAsync(a =>
            bookRequest.AuthorIds.Contains(a.Id)
        );
        if (!authors.Any())
            return (false, "A book must have an associated author.");

        var publisher = await _uow.PublisherRepository.FindByIdAsync(bookRequest.PublisherId);
        if (publisher == null)
            return (false, "A book must have an associated publisher.");

        var primaryGenre = await _uow.GenreRepository.FindByIdAsync(bookRequest.PrimaryGenreId);
        if (primaryGenre == null)
            return (false, "A book must have an associated primary genre.");

        book.Genres = genres.ToList();
        book.Authors = authors.ToList();
        book.Publisher = publisher;
        book.PrimaryGenre = primaryGenre;

        return (true, string.Empty);
    }
}

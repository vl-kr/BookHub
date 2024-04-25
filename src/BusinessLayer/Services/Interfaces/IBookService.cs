using BusinessLayer.DTOs.Requests.Book;
using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.BookFilters;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IBookService
{
    Task<ServiceResult<BookResponse>> CreateBook(BookRequest bookRequest);

    Task<ServiceResult<IEnumerable<BookResponse>>> GetBooks(
        PageOptions pageOptions,
        BookFilter bookFilter
    );

    Task<ServiceResult<BookResponse>> GetBook(int id);
    Task<ServiceResult<BookResponse>> UpdateBook(int id, BookRequest bookRequest);
    Task<ServiceResult<BookResponse>> DeleteBook(int id);
    Task<PaginationObject<Book>> GetPaginatedBooks(PageOptions pageOptions, BookFilter bookFilter);
}

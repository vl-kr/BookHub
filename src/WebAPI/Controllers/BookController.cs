using BusinessLayer.DTOs.Requests.Book;
using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.BookFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BookController : BaseController
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _bookService = bookService;
        SetTokenSource(TokenSourceConstants.BooksTokenSource);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookRequest newBook)
    {
        var serviceResult = await _bookService.CreateBook(newBook);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.BooksTokenSource
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PageOptions pageOptions,
        [FromQuery] BookFilter bookFilter
    )
    {
        var cacheKey = CreateCacheKeyFromPageOptions(pageOptions, "Books");
        var isFilterEmpty = bookFilter
            .GetType()
            .GetProperties()
            .All(prop => prop.GetValue(bookFilter) == null);
        var shouldCache = pageOptions.SearchTerm == null && isFilterEmpty;

        if (
            shouldCache
            && MemoryCache.TryGetValue(
                cacheKey,
                out ServiceResult<IEnumerable<BookResponse>>? serviceResult
            )
        )
            return BuildResponse(serviceResult);
        serviceResult = await _bookService.GetBooks(pageOptions, bookFilter);

        if (shouldCache)
            SaveSource(serviceResult, TokenSourceConstants.BooksTokenSource, cacheKey);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var cacheKey = $"BooksDetail_{id}";
        if (MemoryCache.TryGetValue(cacheKey, out ServiceResult<BookResponse>? serviceResult))
            return BuildResponse(serviceResult);
        serviceResult = await _bookService.GetBook(id);

        SaveSource(serviceResult, TokenSourceConstants.BooksTokenSource, cacheKey);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookRequest updatedBook)
    {
        var serviceResult = await _bookService.UpdateBook(id, updatedBook);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.BooksTokenSource
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _bookService.DeleteBook(id);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.BooksTokenSource
        );
    }
}

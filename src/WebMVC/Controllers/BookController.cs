using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.BookFilters;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models.Book;

namespace WebMVC.Controllers;

[Authorize]
public class BookController : BaseController
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookController(IBookService bookService, IMapper mapper, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _bookService = bookService;
        _mapper = mapper;

        SetTokenSource(TokenSourceConstants.BooksTokenSource);
    }

    public async Task<IActionResult> Index(
        [FromQuery] PageOptions options,
        [FromQuery] BookFilter bookFilter
    )
    {
        var cacheKey = CreateCacheKeyFromPageOptions(options, "Books");
        var isFilterEmpty = bookFilter
            .GetType()
            .GetProperties()
            .All(prop => prop.GetValue(bookFilter) == null);
        var shouldCache = options.SearchTerm == null && isFilterEmpty;

        if (shouldCache && MemoryCache.TryGetValue(cacheKey, out PaginationObject<Book>? books))
            return View(books);

        books = await _bookService.GetPaginatedBooks(options, bookFilter);

        if (shouldCache)
            SaveSource(books, TokenSourceConstants.BooksTokenSource, cacheKey);

        return View(books);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var cacheKey = $"BookDetail_{id}";
        if (MemoryCache.TryGetValue(cacheKey, out BookDetailViewModel? cachedBook))
            return View(cachedBook);

        var bookResult = await _bookService.GetBook(id);
        var handleResult = HandleReadResult(bookResult);
        if (handleResult != null)
            return handleResult;

        cachedBook = _mapper.Map<BookDetailViewModel>(bookResult.Data);

        SaveSource(cachedBook, TokenSourceConstants.BooksTokenSource, cacheKey);

        return View(cachedBook);
    }

    public async Task<IActionResult> AddToShoppingCart(int id)
    {
        var bookResult = await _bookService.GetBook(id);

        var handleResult = HandleReadResult(bookResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(nameof(Index), nameof(BookController).Replace("Controller", ""));
    }
}

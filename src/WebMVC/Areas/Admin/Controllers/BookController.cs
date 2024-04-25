using System.Net;
using AutoMapper;
using BusinessLayer.DTOs.Requests.Book;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Areas.Admin.Models.Book;

namespace WebMVC.Areas.Admin.Controllers;

public class BookController : AdminController
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookController(IBookService bookService, IMapper mapper, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var bookResult = await _bookService.CreateBook(_mapper.Map<BookRequest>(model));

        var handleResult = HandleCreateResult(bookResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.BooksTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(BookController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Edit(int id)
    {
        var bookResult = await _bookService.GetBook(id);
        var handleResult = HandleReadResult(bookResult);
        if (handleResult != null)
            return handleResult;

        var book = bookResult.Data;
        if (book?.PrimaryGenre == null || book.Publisher == null)
            return HandleError("Book has invalid data", HttpStatusCode.InternalServerError);
        var updateModel = _mapper.Map<BookUpdateViewModel>(book);

        updateModel.AuthorIds = book.Authors.Select(x => x.Id).ToList();
        updateModel.GenreIds = book.Genres.Select(x => x.Id).ToList();
        updateModel.PrimaryGenreId = book.PrimaryGenre.Id;
        updateModel.PublisherId = book.Publisher.Id;

        return View(updateModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, BookUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var editResult = await _bookService.UpdateBook(id, _mapper.Map<BookRequest>(model));

        var handleResult = HandleEditResult(editResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.BooksTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(BookController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _bookService.DeleteBook(id);
        var handleResult = HandleDeleteResult(deleteResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.BooksTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(BookController).Replace("Controller", ""),
            new { Area = "" }
        );
    }
}

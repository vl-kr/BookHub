using AutoMapper;
using BusinessLayer.DTOs.Requests.Author;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Areas.Admin.Models.Author;

namespace WebMVC.Areas.Admin.Controllers;

public class AuthorController : AdminController
{
    private readonly IAuthorService _authorService;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorService authorService, IMapper mapper, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _authorService = authorService;
        _mapper = mapper;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var authorResult = await _authorService.CreateAuthor(_mapper.Map<AuthorRequest>(model));

        var handleResult = HandleCreateResult(authorResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.AuthorsTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(AuthorController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Edit(int id)
    {
        var authorResult = await _authorService.GetAuthor(id);
        var handleResult = HandleReadResult(authorResult);
        if (handleResult != null)
            return handleResult;

        var author = authorResult.Data;

        return View(_mapper.Map<AuthorUpdateViewModel>(author));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, AuthorUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var authorResult = await _authorService.UpdateAuthor(id, _mapper.Map<AuthorRequest>(model));

        var handleResult = HandleEditResult(authorResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.AuthorsTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(AuthorController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _authorService.DeleteAuthor(id);
        var handleResult = HandleDeleteResult(deleteResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.AuthorsTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(AuthorController).Replace("Controller", ""),
            new { Area = "" }
        );
    }
}

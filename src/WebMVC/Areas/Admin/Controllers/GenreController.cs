using AutoMapper;
using BusinessLayer.DTOs.Requests.Genre;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Areas.Admin.Models.Genre;

namespace WebMVC.Areas.Admin.Controllers;

public class GenreController : AdminController
{
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public GenreController(IGenreService genreService, IMapper mapper, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _genreService = genreService;
        _mapper = mapper;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GenreCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var genreResult = await _genreService.CreateGenre(_mapper.Map<GenreRequest>(model));

        var handleResult = HandleCreateResult(genreResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.GenresTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(GenreController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Edit(int id)
    {
        var genreResult = await _genreService.GetGenre(id);
        var handleResult = HandleReadResult(genreResult);
        if (handleResult != null)
            return handleResult;

        var genre = genreResult.Data;

        return View(_mapper.Map<GenreUpdateViewModel>(genre));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, GenreUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var genreResult = await _genreService.UpdateGenre(id, _mapper.Map<GenreRequest>(model));

        var handleResult = HandleEditResult(genreResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.GenresTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(GenreController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _genreService.DeleteGenre(id);
        var handleResult = HandleDeleteResult(deleteResult);
        if (handleResult != null)
            return handleResult;

        InvalidateTokenSource(TokenSourceConstants.GenresTokenSource);

        return RedirectToAction(
            nameof(Index),
            nameof(GenreController).Replace("Controller", ""),
            new { Area = "" }
        );
    }
}

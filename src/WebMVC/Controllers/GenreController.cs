using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebMVC.Controllers;

[Authorize]
public class GenreController : BaseController
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _genreService = genreService;
        SetTokenSource(TokenSourceConstants.GenresTokenSource);
    }

    public async Task<IActionResult> Index([FromQuery] PageOptions options)
    {
        var cacheKey = CreateCacheKeyFromPageOptions(options, "Genres");
        var shouldCache = options.SearchTerm == null;

        if (shouldCache && MemoryCache.TryGetValue(cacheKey, out PaginationObject<Genre>? genres))
            return View(genres);

        genres = await _genreService.GetPaginatedGenres(options);

        if (shouldCache)
            SaveSource(genres, TokenSourceConstants.GenresTokenSource, cacheKey);
        return View(genres);
    }
}

using BusinessLayer.DTOs.Requests.Genre;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.GenreFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GenreController : BaseController
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _genreService = genreService;
        SetTokenSource(TokenSourceConstants.GenresTokenSource);
    }

    [HttpPost]
    public async Task<IActionResult> Create(GenreRequest newGenre)
    {
        var serviceResult = await _genreService.CreateGenre(newGenre);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.GenresTokenSource
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PageOptions pageOptions,
        [FromQuery] GenreFilter genreFilter
    )
    {
        var cacheKey = CreateCacheKeyFromPageOptions(pageOptions, "Genres");
        var isFilterEmpty = genreFilter
            .GetType()
            .GetProperties()
            .All(prop => prop.GetValue(genreFilter) == null);
        var shouldCache = pageOptions.SearchTerm == null && isFilterEmpty;

        if (
            shouldCache
            && MemoryCache.TryGetValue(
                cacheKey,
                out ServiceResult<IEnumerable<GenreResponse>>? serviceResult
            )
        )
            return BuildResponse(serviceResult);
        serviceResult = await _genreService.GetGenres(pageOptions, genreFilter);

        if (shouldCache)
            SaveSource(serviceResult, TokenSourceConstants.GenresTokenSource, cacheKey);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _genreService.GetGenre(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GenreRequest updatedGenre)
    {
        var serviceResult = await _genreService.UpdateGenre(id, updatedGenre);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.GenresTokenSource
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _genreService.DeleteGenre(id);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.GenresTokenSource
        );
    }
}

using BusinessLayer.DTOs.Requests.Author;
using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.AuthorFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuthorController : BaseController
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _authorService = authorService;
        SetTokenSource(TokenSourceConstants.AuthorsTokenSource);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorRequest newAuthor)
    {
        var serviceResult = await _authorService.CreateAuthor(newAuthor);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.AuthorsTokenSource
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PageOptions pageOptions,
        [FromQuery] AuthorFilter authorFilter
    )
    {
        var cacheKey = CreateCacheKeyFromPageOptions(pageOptions, "Authors");
        var isFilterEmpty = authorFilter
            .GetType()
            .GetProperties()
            .All(prop => prop.GetValue(authorFilter) == null);
        var shouldCache = pageOptions.SearchTerm == null && isFilterEmpty;

        if (
            shouldCache
            && MemoryCache.TryGetValue(
                cacheKey,
                out ServiceResult<IEnumerable<AuthorResponse>>? serviceResult
            )
        )
            return BuildResponse(serviceResult);
        serviceResult = await _authorService.GetAuthors(pageOptions, authorFilter);

        if (shouldCache)
            SaveSource(serviceResult, TokenSourceConstants.AuthorsTokenSource, cacheKey);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _authorService.GetAuthor(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AuthorRequest updatedAuthor)
    {
        var serviceResult = await _authorService.UpdateAuthor(id, updatedAuthor);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.AuthorsTokenSource
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _authorService.DeleteAuthor(id);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.AuthorsTokenSource
        );
    }
}

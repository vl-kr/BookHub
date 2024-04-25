using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebMVC.Controllers;

[Authorize]
public class AuthorController : BaseController
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _authorService = authorService;
        SetTokenSource(TokenSourceConstants.AuthorsTokenSource);
    }

    public async Task<IActionResult> Index([FromQuery] PageOptions options)
    {
        var cacheKey = CreateCacheKeyFromPageOptions(options, "Authors");
        var shouldCache = options.SearchTerm == null;

        if (shouldCache && MemoryCache.TryGetValue(cacheKey, out PaginationObject<Author>? authors))
            return View(authors);

        authors = await _authorService.GetPaginatedAuthors(options);

        if (shouldCache)
            SaveSource(authors, TokenSourceConstants.AuthorsTokenSource, cacheKey);
        return View(authors);
    }
}

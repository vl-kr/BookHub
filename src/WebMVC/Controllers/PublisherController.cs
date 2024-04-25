using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebMVC.Controllers;

[Authorize]
public class PublisherController : BaseController
{
    private readonly IPublisherService _publisherService;

    public PublisherController(IPublisherService publisherService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _publisherService = publisherService;
        SetTokenSource(TokenSourceConstants.PublishersTokenSource);
    }

    public async Task<IActionResult> Index([FromQuery] PageOptions options)
    {
        var cacheKey = CreateCacheKeyFromPageOptions(options, "Publishers");
        var shouldCache = options.SearchTerm == null;

        if (
            shouldCache
            && MemoryCache.TryGetValue(cacheKey, out PaginationObject<Publisher>? publishers)
        )
            return View(publishers);

        publishers = await _publisherService.GetPaginatedPublishers(options);

        if (shouldCache)
            SaveSource(publishers, TokenSourceConstants.PublishersTokenSource, cacheKey);
        return View(publishers);
    }
}

using BusinessLayer.DTOs.Requests.Publisher;
using BusinessLayer.DTOs.Responses.Publisher;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.PublisherFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PublisherController : BaseController
{
    private readonly IPublisherService _publisherService;

    public PublisherController(IPublisherService publisherService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _publisherService = publisherService;
        SetTokenSource(TokenSourceConstants.PublishersTokenSource);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PublisherRequest newPublisher)
    {
        var serviceResult = await _publisherService.CreatePublisher(newPublisher);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.PublishersTokenSource
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PageOptions pageOptions,
        [FromQuery] PublisherFilter publisherFilter
    )
    {
        var cacheKey = CreateCacheKeyFromPageOptions(pageOptions, "Publishers");
        var isFilterEmpty = publisherFilter
            .GetType()
            .GetProperties()
            .All(prop => prop.GetValue(publisherFilter) == null);
        var shouldCache = pageOptions.SearchTerm == null && isFilterEmpty;

        if (
            shouldCache
            && MemoryCache.TryGetValue(
                cacheKey,
                out ServiceResult<IEnumerable<PublisherResponse>>? serviceResult
            )
        )
            return BuildResponse(serviceResult);
        serviceResult = await _publisherService.GetPublishers(pageOptions, publisherFilter);

        if (shouldCache)
            SaveSource(serviceResult, TokenSourceConstants.PublishersTokenSource, cacheKey);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _publisherService.GetPublisher(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PublisherRequest updatedPublisher)
    {
        var serviceResult = await _publisherService.UpdatePublisher(id, updatedPublisher);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.PublishersTokenSource
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _publisherService.DeletePublisher(id);
        return MaybeInvalidateSourceAndBuildResponse(
            serviceResult,
            TokenSourceConstants.PublishersTokenSource
        );
    }
}

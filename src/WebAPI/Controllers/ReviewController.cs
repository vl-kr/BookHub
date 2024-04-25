using BusinessLayer.DTOs.Requests.Review;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.ReviewFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ReviewController : BaseController
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReviewRequest newReview)
    {
        var serviceResult = await _reviewService.CreateReview(newReview);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PageOptions pageOptions,
        [FromQuery] ReviewFilter reviewFilter
    )
    {
        var serviceResult = await _reviewService.GetReviews(pageOptions, reviewFilter);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _reviewService.GetReview(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReviewRequest updatedReview)
    {
        var serviceResult = await _reviewService.UpdateReview(id, updatedReview);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _reviewService.DeleteReview(id);
        return BuildResponse(serviceResult);
    }
}

using System.Net;
using AutoMapper;
using BusinessLayer.DTOs.Requests.Review;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models.Review;

namespace WebMVC.Controllers;

[Authorize]
public class ReviewController : BaseController
{
    private readonly IAuthService _authService;
    private readonly ILocalIdentityUserService _localIdentityUserService;
    private readonly IMapper _mapper;
    private readonly IReviewService _reviewService;

    public ReviewController(
        IReviewService reviewService,
        IAuthService authService,
        IMapper mapper,
        ILocalIdentityUserService localIdentityUserService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _reviewService = reviewService;
        _authService = authService;
        _mapper = mapper;
        _localIdentityUserService = localIdentityUserService;
    }

    public async Task<IActionResult> AddReview(int id, ReviewCreateViewModel model)
    {
        var user = await _authService.GetUserAsync(User);
        if (user == null)
            return HandleError("User not found", HttpStatusCode.InternalServerError);

        var userResult = await _localIdentityUserService.GetLocalIdentityUser(user.Id);
        var handleResult = HandleReadResult(userResult);
        if (handleResult != null)
            return handleResult;

        if (userResult.Data?.Customer == null)
            return HandleError("Customer not found", HttpStatusCode.InternalServerError);

        await _reviewService.CreateReview(
            new ReviewRequest
            {
                BookId = id,
                CustomerId = userResult.Data.Customer.Id,
                Rating = model.Rating,
                TextReview = model.TextReview
            }
        );

        MemoryCache.Remove($"BookDetail_{id}");

        return RedirectToAction(
            "Detail",
            nameof(BookController).Replace("Controller", ""),
            new { id }
        );
    }

    public async Task<IActionResult> RemoveReview(int id, int bookId)
    {
        await _reviewService.DeleteReview(id);

        MemoryCache.Remove($"BookDetail_{bookId}");

        return RedirectToAction(
            "Detail",
            nameof(BookController).Replace("Controller", ""),
            new { id = bookId }
        );
    }
}

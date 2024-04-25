using BusinessLayer.DTOs.Requests.Wishlist;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class WishlistController : BaseController
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _wishlistService = wishlistService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(WishlistRequest newWishlist)
    {
        var serviceResult = await _wishlistService.CreateWishlist(newWishlist);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _wishlistService.GetWishlists(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _wishlistService.GetWishlist(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] WishlistRequest updatedWishlist)
    {
        var serviceResult = await _wishlistService.UpdateWishlist(id, updatedWishlist);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _wishlistService.DeleteWishlist(id);
        return BuildResponse(serviceResult);
    }
}

using BusinessLayer.DTOs.Requests.WishlistItem;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class WishlistItemController : BaseController
{
    private readonly IWishlistItemService _wishlistItemService;

    public WishlistItemController(
        IWishlistItemService wishlistItemService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _wishlistItemService = wishlistItemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(WishlistItemRequest newWishlistItem)
    {
        var serviceResult = await _wishlistItemService.CreateWishlistItem(newWishlistItem);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _wishlistItemService.GetWishlistItems(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _wishlistItemService.GetWishlistItem(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] WishlistItemRequest updatedWishlistItem
    )
    {
        var serviceResult = await _wishlistItemService.UpdateWishlistItem(id, updatedWishlistItem);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _wishlistItemService.DeleteWishlistItem(id);
        return BuildResponse(serviceResult);
    }
}

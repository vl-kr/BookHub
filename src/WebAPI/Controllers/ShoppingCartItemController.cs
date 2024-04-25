using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ShoppingCartItemController : BaseController
{
    private readonly IShoppingCartItemService _shoppingCartItemService;

    public ShoppingCartItemController(
        IShoppingCartItemService shoppingCartItemService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _shoppingCartItemService = shoppingCartItemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ShoppingCartItemRequest newShoppingCartItem)
    {
        var serviceResult = await _shoppingCartItemService.CreateShoppingCartItem(
            newShoppingCartItem
        );
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _shoppingCartItemService.GetShoppingCartItems(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _shoppingCartItemService.GetShoppingCartItem(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] ShoppingCartItemRequest updatedShoppingCartItem
    )
    {
        var serviceResult = await _shoppingCartItemService.UpdateShoppingCartItem(
            id,
            updatedShoppingCartItem
        );
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _shoppingCartItemService.DeleteShoppingCartItem(id);
        return BuildResponse(serviceResult);
    }
}

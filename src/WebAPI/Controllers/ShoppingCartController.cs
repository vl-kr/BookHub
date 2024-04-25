using BusinessLayer.DTOs.Requests.ShoppingCart;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ShoppingCartController : BaseController
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(
        IShoppingCartService shoppingCartService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ShoppingCartRequest newShoppingCart)
    {
        var serviceResult = await _shoppingCartService.CreateShoppingCart(newShoppingCart);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _shoppingCartService.GetShoppingCarts(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _shoppingCartService.GetShoppingCart(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] ShoppingCartRequest updatedShoppingCart
    )
    {
        var serviceResult = await _shoppingCartService.UpdateShoppingCart(id, updatedShoppingCart);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _shoppingCartService.DeleteShoppingCart(id);
        return BuildResponse(serviceResult);
    }
}

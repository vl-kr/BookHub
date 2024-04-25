using System.Net;
using AutoMapper;
using BusinessLayer.Coordinators.Interfaces;
using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.Enums;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models.ShoppingCart;

namespace WebMVC.Controllers;

[Authorize]
public class ShoppingCartController : BaseController
{
    private readonly IAuthService _authService;
    private readonly ICartToOrderCoordinator _cartToOrderCoordinator;
    private readonly ILocalIdentityUserService _localIdentityUserService;
    private readonly IMapper _mapper;
    private readonly IShoppingCartItemService _shoppingCartItemService;
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(
        IShoppingCartService shoppingCartService,
        IShoppingCartItemService shoppingCartItemService,
        IMapper mapper,
        ICartToOrderCoordinator cartToOrderCoordinator,
        IAuthService authService,
        ILocalIdentityUserService localIdentityUserService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _shoppingCartService = shoppingCartService;
        _shoppingCartItemService = shoppingCartItemService;
        _cartToOrderCoordinator = cartToOrderCoordinator;
        _authService = authService;
        _localIdentityUserService = localIdentityUserService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Detail()
    {
        var shoppingCart = await GetUsersShoppingCart();
        if (shoppingCart == null)
            return HandleError("Cart not found", HttpStatusCode.InternalServerError);

        return View(_mapper.Map<ShoppingCartDetailViewModel>(shoppingCart));
    }

    public async Task<IActionResult> AddBookToCart(int id)
    {
        var shoppingCart = await GetUsersShoppingCart();
        if (shoppingCart == null)
            return HandleError("Cart not found", HttpStatusCode.InternalServerError);

        foreach (var item in shoppingCart.ShoppingCartItems)
            if (item.BookId == id)
                return await ChangeItemQuantity(item.Id, item.Quantity + 1);

        var newShoppingCartItemResult = await _shoppingCartItemService.CreateShoppingCartItem(
            new ShoppingCartItemRequest
            {
                BookId = id,
                Quantity = 1,
                ShoppingCartId = shoppingCart.Id
            }
        );

        var handleResult = HandleCreateResult(newShoppingCartItemResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(
            nameof(Detail),
            nameof(ShoppingCartController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> DeleteItemFromCart(int id)
    {
        await _shoppingCartItemService.DeleteShoppingCartItem(id);

        return RedirectToAction(
            nameof(Detail),
            nameof(ShoppingCartController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> ChangeItemQuantity(int id, int newQuantity)
    {
        var cartItemResult = await _shoppingCartItemService.ChangeQuantity(id, newQuantity);
        var handleResult = HandleEditResult(cartItemResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(
            nameof(Detail),
            nameof(ShoppingCartController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> CreateOrder(int id)
    {
        var shoppingCart = await GetUsersShoppingCart();
        if (shoppingCart == null)
            return HandleError("Cart not found", HttpStatusCode.InternalServerError);

        var orderResponse = await _cartToOrderCoordinator.CreateOrderFromCartAsync(shoppingCart.Id);
        if (orderResponse == null)
            return HandleError("Order creation failed", HttpStatusCode.InternalServerError);

        return RedirectToAction(nameof(Index), nameof(OrderController).Replace("Controller", ""));
    }

    private async Task<ShoppingCart?> GetUsersShoppingCart()
    {
        var user = await _authService.GetUserAsync(User);
        if (user == null)
            return null;

        var userResult = await _localIdentityUserService.GetLocalIdentityUser(user.Id);
        if (userResult.StatusCode != ServiceResultCode.OK)
            return null;

        if (userResult.Data?.Customer == null)
            return null;

        var shoppingCart = await _shoppingCartService.GetShoppingCartOfCustomer(
            userResult.Data.Customer.Id
        );
        return shoppingCart;
    }
}

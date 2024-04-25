using System.Net;
using AutoMapper;
using BusinessLayer.Coordinators.Interfaces;
using BusinessLayer.DTOs.Requests.WishlistItem;
using BusinessLayer.Enums;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models.Wishlist;

namespace WebMVC.Controllers;

[Authorize]
public class WishlistController : BaseController
{
    private readonly IAuthService _authService;
    private readonly ILocalIdentityUserService _localIdentityUserService;
    private readonly IMapper _mapper;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IWishlistItemService _wishlistItemService;
    private readonly IWishlistService _wishlistService;
    private readonly IWishlistToCartCoordinator _wishlistToCartCoordinator;

    public WishlistController(
        IWishlistService wishlistService,
        IWishlistItemService wishlistItemService,
        IShoppingCartService shoppingCartService,
        IMapper mapper,
        IWishlistToCartCoordinator wishlistToCartCoordinator,
        IAuthService authService,
        ILocalIdentityUserService localIdentityUserService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _wishlistService = wishlistService;
        _wishlistItemService = wishlistItemService;
        _shoppingCartService = shoppingCartService;
        _mapper = mapper;
        _wishlistToCartCoordinator = wishlistToCartCoordinator;
        _authService = authService;
        _localIdentityUserService = localIdentityUserService;
    }

    public async Task<IActionResult> Detail()
    {
        var wishlist = await GetUsersWishlist();
        if (wishlist == null)
            return HandleError("Wishlist not found", HttpStatusCode.InternalServerError);

        return View(_mapper.Map<WishlistDetailViewModel>(wishlist));
    }

    public async Task<IActionResult> AddBookToWishlist(int id)
    {
        var wishlist = await GetUsersWishlist();
        if (wishlist == null)
            return HandleError("Wishlist not found", HttpStatusCode.InternalServerError);

        var newWishlistItemResult = await _wishlistItemService.CreateWishlistItem(
            new WishlistItemRequest { BookId = id, WishlistId = wishlist.Id }
        );

        var handleResult = HandleCreateResult(newWishlistItemResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(
            nameof(Detail),
            nameof(WishlistController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> DeleteItemFromWishlist(int id)
    {
        var deleteResult = await _wishlistItemService.DeleteWishlistItem(id);
        var handleResult = HandleDeleteResult(deleteResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(
            nameof(Detail),
            nameof(WishlistController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> DeleteBookFromWishlist(int id)
    {
        var wishlist = await GetUsersWishlist();
        if (wishlist == null)
            return HandleError("Wishlist not found", HttpStatusCode.InternalServerError);

        var itemResult = await _wishlistItemService.GetWishlistItemOfBook(wishlist.Id, id);
        var handleItemResult = HandleReadResult(itemResult);
        if (handleItemResult != null)
            return handleItemResult;

        if (itemResult.Data == null)
            return HandleError("Wishlist item not found", HttpStatusCode.InternalServerError);

        var deleteResult = await _wishlistItemService.DeleteWishlistItem(itemResult.Data.Id);
        var handleDeleteResult = HandleDeleteResult(deleteResult);
        if (handleDeleteResult != null)
            return handleDeleteResult;

        return RedirectToAction(
            nameof(Detail),
            nameof(WishlistController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> MoveToCart(int id)
    {
        var cart = await GetUsersShoppingCart();
        if (cart == null)
            return HandleError("Shopping cart not found", HttpStatusCode.InternalServerError);

        var convertingSuccessful =
            await _wishlistToCartCoordinator.AddSingleFromWishlistToCartAsync(id, cart.Id);
        if (!convertingSuccessful)
            return HandleError("Moving to cart failed", HttpStatusCode.InternalServerError);

        return RedirectToAction(
            nameof(Detail),
            nameof(ShoppingCartController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> MoveAllToCart()
    {
        var wishlist = await GetUsersWishlist();
        if (wishlist == null)
            return HandleError("Wishlist not found", HttpStatusCode.InternalServerError);

        var cart = await GetUsersShoppingCart();
        if (cart == null)
            return HandleError("Shopping cart not found", HttpStatusCode.InternalServerError);

        var convertingSuccessful = await _wishlistToCartCoordinator.AddAllFromWishlistToCartAsync(
            wishlist.Id,
            cart.Id
        );
        if (!convertingSuccessful)
            return HandleError("Moving to cart failed", HttpStatusCode.InternalServerError);

        return RedirectToAction(
            nameof(Detail),
            nameof(ShoppingCartController).Replace("Controller", "")
        );
    }

    private async Task<Wishlist?> GetUsersWishlist()
    {
        var user = await _authService.GetUserAsync(User);
        if (user == null)
            return null;

        var userResult = await _localIdentityUserService.GetLocalIdentityUser(user.Id);
        if (userResult.StatusCode != ServiceResultCode.OK || userResult.Data?.Customer == null)
            return null;

        var wishlist = await _wishlistService.GetWishlistOfCustomer(userResult.Data.Customer.Id);
        return wishlist;
    }

    private async Task<ShoppingCart?> GetUsersShoppingCart()
    {
        var user = await _authService.GetUserAsync(User);
        if (user == null)
            return null;

        var userResult = await _localIdentityUserService.GetLocalIdentityUser(user.Id);
        if (userResult.StatusCode != ServiceResultCode.OK || userResult.Data?.Customer == null)
            return null;

        var shoppingCart = await _shoppingCartService.GetShoppingCartOfCustomer(
            userResult.Data.Customer.Id
        );
        return shoppingCart;
    }
}

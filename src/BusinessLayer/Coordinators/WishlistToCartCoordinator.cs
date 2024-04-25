using AutoMapper;
using BusinessLayer.Coordinators.Interfaces;
using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.Enums;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.UnitOfWork;

namespace BusinessLayer.Coordinators;

public class WishlistToCartCoordinator : IWishlistToCartCoordinator
{
    private readonly IMapper _mapper;
    private readonly IShoppingCartItemService _shoppingCartItemService;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWishlistItemService _wishlistItemService;
    private readonly IWishlistService _wishlistService;

    public WishlistToCartCoordinator(
        IShoppingCartService shoppingCartService,
        IShoppingCartItemService shoppingCartItemService,
        IWishlistItemService wishlistItemService,
        IWishlistService wishlistService,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _shoppingCartItemService = shoppingCartItemService;
        _shoppingCartService = shoppingCartService;
        _wishlistItemService = wishlistItemService;
        _wishlistService = wishlistService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddAllFromWishlistToCartAsync(int wishlistId, int cartId)
    {
        var wishlistResult = await _wishlistService.GetWishlist(wishlistId);
        if (wishlistResult.StatusCode != ServiceResultCode.OK || wishlistResult.Data == null)
            return false;

        var wishlist = wishlistResult.Data;

        await using var transaction = _unitOfWork.BeginTransaction();

        foreach (var item in wishlist.WishlistItems)
        {
            var result = await AddItemFromWishlistToCartAsync(item.Id, cartId);
            if (!result)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        await transaction.CommitAsync();
        return true;
    }

    public async Task<bool> AddSingleFromWishlistToCartAsync(int wishlistItemId, int cartId)
    {
        await using var transaction = _unitOfWork.BeginTransaction();
        var result = await AddItemFromWishlistToCartAsync(wishlistItemId, cartId);
        if (!result)
        {
            await transaction.RollbackAsync();
            return false;
        }

        await transaction.CommitAsync();
        return true;
    }

    private async Task<bool> AddItemFromWishlistToCartAsync(int wishlistItemId, int cartId)
    {
        var wishlistItemResult = await _wishlistItemService.GetWishlistItem(wishlistItemId);
        if (
            wishlistItemResult.StatusCode != ServiceResultCode.OK
            || wishlistItemResult.Data == null
        )
            return false;

        var cartResult = await _shoppingCartService.GetShoppingCart(cartId);
        if (cartResult.StatusCode != ServiceResultCode.OK || cartResult.Data == null)
            return false;

        var wishlistItem = wishlistItemResult.Data;
        var cart = cartResult.Data;

        var cartItemRequest = _mapper.Map<ShoppingCartItemRequest>(wishlistItem);
        cartItemRequest.ShoppingCartId = cartId;

        var bookCartItem = cart.ShoppingCartItems.FirstOrDefault(x =>
            x.Book != null && wishlistItem.Book != null && x.Book.Id == wishlistItem.Book.Id
        );

        if (bookCartItem == null)
        {
            cartItemRequest.Quantity = 1;
            var cartItem = await _shoppingCartItemService.CreateShoppingCartItem(cartItemRequest);
            if (cartItem.StatusCode != ServiceResultCode.Created)
                return false;
        }
        else
        {
            var cartItem = await _shoppingCartItemService.ChangeQuantity(
                bookCartItem.Id,
                bookCartItem.Quantity + 1
            );
            if (cartItem.StatusCode != ServiceResultCode.OK)
                return false;
        }

        var deleteResult = await _wishlistItemService.DeleteWishlistItem(wishlistItem.Id);
        if (deleteResult.StatusCode != ServiceResultCode.NoContent)
            return false;

        return true;
    }
}

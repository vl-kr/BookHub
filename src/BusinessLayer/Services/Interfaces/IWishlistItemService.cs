using BusinessLayer.DTOs.Requests.WishlistItem;
using BusinessLayer.DTOs.Responses.WishlistItem;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IWishlistItemService
{
    Task<ServiceResult<WishlistItemResponse>> CreateWishlistItem(
        WishlistItemRequest wishlistItemRequest
    );

    Task<ServiceResult<IEnumerable<WishlistItemResponse>>> GetWishlistItems(
        PageOptions pageOptions
    );

    Task<ServiceResult<WishlistItemResponse>> GetWishlistItem(int id);

    Task<ServiceResult<WishlistItemResponse>> UpdateWishlistItem(
        int id,
        WishlistItemRequest wishlistItemRequest
    );

    Task<ServiceResult<WishlistItemResponse>> DeleteWishlistItem(int id);
    Task<ServiceResult<WishlistItemResponse?>> GetWishlistItemOfBook(int id, int bookId);
}

using BusinessLayer.DTOs.Requests.Wishlist;
using BusinessLayer.DTOs.Responses.Wishlist;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IWishlistService
{
    Task<ServiceResult<WishlistResponse>> CreateWishlist(WishlistRequest wishlistRequest);
    Task<ServiceResult<IEnumerable<WishlistResponse>>> GetWishlists(PageOptions pageOptions);
    Task<ServiceResult<WishlistResponse>> GetWishlist(int id);
    Task<ServiceResult<WishlistResponse>> UpdateWishlist(int id, WishlistRequest wishlistRequest);
    Task<ServiceResult<WishlistResponse>> DeleteWishlist(int id);
    Task<Wishlist?> GetWishlistOfCustomer(int customerId);
}

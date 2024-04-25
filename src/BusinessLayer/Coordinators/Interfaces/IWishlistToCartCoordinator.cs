namespace BusinessLayer.Coordinators.Interfaces;

public interface IWishlistToCartCoordinator
{
    Task<bool> AddAllFromWishlistToCartAsync(int wishlistId, int cartId);
    Task<bool> AddSingleFromWishlistToCartAsync(int wishlistItemId, int cartId);
}

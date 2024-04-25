using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.DTOs.Responses.WishlistItem;

namespace BusinessLayer.DTOs.Responses.Wishlist;

public class WishlistResponse : BaseResponse
{
    public CustomerBasicInfoResponse? Customer { get; set; }

    public IEnumerable<WishlistItemResponse> WishlistItems { get; set; } =
        new List<WishlistItemResponse>();
}

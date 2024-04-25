using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.DTOs.Responses.WishlistItem;

namespace WebMVC.Models.Wishlist;

public class WishlistDetailViewModel
{
    public int Id { get; set; }
    public CustomerBasicInfoResponse? Customer { get; set; }
    public IEnumerable<WishlistItemResponse> WishlistItems { get; set; } =
        new List<WishlistItemResponse>();
}

namespace BusinessLayer.DTOs.Requests.WishlistItem;

public class WishlistItemRequest : BaseRequest
{
    public int BookId { get; set; }
    public int WishlistId { get; set; }
}

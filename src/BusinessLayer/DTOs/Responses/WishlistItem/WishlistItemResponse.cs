using BusinessLayer.DTOs.Responses.Book;

namespace BusinessLayer.DTOs.Responses.WishlistItem;

public class WishlistItemResponse : BaseResponse
{
    public BookBasicInfoResponse? Book { get; set; }
}

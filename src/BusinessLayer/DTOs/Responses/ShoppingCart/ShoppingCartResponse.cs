using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.DTOs.Responses.ShoppingCartItem;

namespace BusinessLayer.DTOs.Responses.ShoppingCart;

public class ShoppingCartResponse : BaseResponse
{
    public decimal TotalPrice { get; set; }
    public CustomerBasicInfoResponse? Customer { get; set; }

    public IEnumerable<ShoppingCartItemResponse> ShoppingCartItems { get; set; } =
        new List<ShoppingCartItemResponse>();
}

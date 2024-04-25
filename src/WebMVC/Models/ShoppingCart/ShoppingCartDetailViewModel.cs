using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.DTOs.Responses.ShoppingCartItem;

namespace WebMVC.Models.ShoppingCart;

public class ShoppingCartDetailViewModel
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public CustomerBasicInfoResponse? Customer { get; set; }

    public IEnumerable<ShoppingCartItemResponse> ShoppingCartItems { get; set; } =
        new List<ShoppingCartItemResponse>();
}

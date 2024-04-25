namespace BusinessLayer.DTOs.Requests.ShoppingCartItem;

public class ShoppingCartItemRequest : BaseRequest
{
    public int Quantity { get; set; }
    public int BookId { get; set; }
    public int ShoppingCartId { get; set; }
}

namespace BusinessLayer.DTOs.Requests.OrderItem;

public class OrderItemRequest : BaseRequest
{
    public int Quantity { get; set; }
    public int BookId { get; set; }
    public int? OrderId { get; set; }
}

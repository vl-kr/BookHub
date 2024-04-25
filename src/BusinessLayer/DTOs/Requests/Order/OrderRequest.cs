namespace BusinessLayer.DTOs.Requests.Order;

public class OrderRequest : BaseRequest
{
    public int? OrderStatusId { get; set; }
    public int? CustomerId { get; set; }
    public int? ShippingAddressId { get; set; }
    public int? BillingAddressId { get; set; }
    public int? PaymentMethodId { get; set; }
    public int? ShippingMethodId { get; set; }

    public IEnumerable<int> OrderItemIds { get; set; } = new List<int>();
}

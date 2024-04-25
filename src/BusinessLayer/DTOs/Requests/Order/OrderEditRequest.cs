namespace BusinessLayer.DTOs.Requests.Order;

public class OrderEditRequest : BaseRequest
{
    public int? OrderStatusId { get; set; }
    public int? ShippingAddressId { get; set; }
    public int? BillingAddressId { get; set; }
    public int? PaymentMethodId { get; set; }
    public int? ShippingMethodId { get; set; }
}

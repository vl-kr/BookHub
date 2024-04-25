using BusinessLayer.DTOs.Responses.Address;
using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.DTOs.Responses.OrderItem;
using BusinessLayer.DTOs.Responses.OrderStatus;
using BusinessLayer.DTOs.Responses.PaymentMethod;
using BusinessLayer.DTOs.Responses.ShippingMethod;

namespace BusinessLayer.DTOs.Responses.Order;

public class OrderResponse : BaseResponse
{
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public string? Note { get; set; }
    public DateTime OrderDate { get; set; }

    public OrderStatusResponse? Status { get; set; }

    public AddressResponse? ShippingAddress { get; set; }

    public AddressResponse? BillingAddress { get; set; }

    public CustomerBasicInfoResponse? Customer { get; set; }

    public PaymentMethodResponse? PaymentMethod { get; set; }

    public ShippingMethodResponse? ShippingMethod { get; set; }

    public IEnumerable<OrderItemResponse> OrderItems { get; set; } = new List<OrderItemResponse>();
}

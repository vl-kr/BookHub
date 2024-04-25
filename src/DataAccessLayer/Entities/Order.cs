using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Order : BaseEntity
{
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public string? Note { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public int? OrderStatusId { get; set; }

    [ForeignKey("OrderStatusId")]
    public OrderStatus? Status { get; set; }

    public int? ShippingAddressId { get; set; }

    [ForeignKey("ShippingAddressId")]
    public Address? ShippingAddress { get; set; }

    public int? BillingAddressId { get; set; }

    [ForeignKey("BillingAddressId")]
    public Address? BillingAddress { get; set; }

    public int? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    public int? PaymentMethodId { get; set; }

    [ForeignKey("PaymentMethodId")]
    public PaymentMethod? PaymentMethod { get; set; }

    public int? ShippingMethodId { get; set; }

    [ForeignKey("ShippingMethodId")]
    public ShippingMethod? ShippingMethod { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

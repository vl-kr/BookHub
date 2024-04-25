using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Customer : BaseEntity
{
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual LocalIdentityUser? User { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    public ShoppingCart? ShoppingCart { get; set; }
    public Wishlist? Wishlist { get; set; }
    public Address? PreferredShippingAddress { get; set; }
    public Address? PreferredBillingAddress { get; set; }
}

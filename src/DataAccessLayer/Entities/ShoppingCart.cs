using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class ShoppingCart : BaseEntity
{
    [NotMapped]
    public decimal TotalPrice { get; set; }

    public int? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; } =
        new List<ShoppingCartItem>();
}

using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Wishlist : BaseEntity
{
    public int? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    public IEnumerable<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}

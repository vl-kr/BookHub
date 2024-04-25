using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;

[Index(nameof(BookId), nameof(WishlistId), IsUnique = true)]
public class WishlistItem : BaseEntity
{
    public int? BookId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    public int? WishlistId { get; set; }

    [ForeignKey("WishlistId")]
    public Wishlist? Wishlist { get; set; }
}

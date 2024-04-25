using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;

[Index(nameof(BookId), nameof(ShoppingCartId), IsUnique = true)]
public class ShoppingCartItem : BaseEntity
{
    public int Quantity { get; set; }

    [NotMapped]
    public decimal TotalPrice { get; set; }

    public int? BookId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    public int? ShoppingCartId { get; set; }

    [ForeignKey("ShoppingCartId")]
    public ShoppingCart? ShoppingCart { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;

[Index(nameof(BookId), nameof(OrderId), IsUnique = true)]
public class OrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public int? BookId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    public int? OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
}

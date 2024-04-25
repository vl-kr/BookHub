using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;

[Index(nameof(BookId), nameof(CustomerId), IsUnique = true)]
public class Review : BaseEntity
{
    public int Rating { get; set; }
    public string? TextReview { get; set; }

    public int? BookId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    public int? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;

[Index(nameof(BookId), nameof(AuthorId), IsUnique = true)]
public class BookAuthor : BaseEntity
{
    public int? BookId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    public int? AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public Author? Author { get; set; }
}

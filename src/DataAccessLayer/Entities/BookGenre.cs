using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities;

[Index(nameof(BookId), nameof(GenreId), IsUnique = true)]
public class BookGenre : BaseEntity
{
    public int? BookId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    public int? GenreId { get; set; }

    [ForeignKey("GenreId")]
    public Genre? Genre { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ISBN { get; set; }
    public int YearPublished { get; set; }
    public string? ImageUrl { get; set; }

    public int? PublisherId { get; set; }

    [ForeignKey("PublisherId")]
    public Publisher? Publisher { get; set; }

    public int? PrimaryGenreId { get; set; }

    [ForeignKey("PrimaryGenreId")]
    public Genre? PrimaryGenre { get; set; }

    public IEnumerable<Author> Authors { get; set; } = new List<Author>();

    public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();

    public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
}

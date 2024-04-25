namespace BusinessLayer.DTOs.Requests.Book;

public class BookRequest : BaseRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ISBN { get; set; }
    public int YearPublished { get; set; }
    public string? ImageUrl { get; set; }

    public int PublisherId { get; set; }
    public int PrimaryGenreId { get; set; }
    public IEnumerable<int> AuthorIds { get; set; } = new List<int>();
    public IEnumerable<int> GenreIds { get; set; } = new List<int>();
}

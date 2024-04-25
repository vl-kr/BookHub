using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.DTOs.Responses.Publisher;
using BusinessLayer.DTOs.Responses.Review;

namespace WebMVC.Models.Book;

public class BookDetailViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ISBN { get; set; }
    public int YearPublished { get; set; }
    public string? ImageUrl { get; set; }

    public PublisherResponse? Publisher { get; set; }
    public GenreResponse? PrimaryGenre { get; set; }
    public IEnumerable<AuthorResponse> Authors { get; set; } = new List<AuthorResponse>();
    public IEnumerable<GenreResponse> Genres { get; set; } = new List<GenreResponse>();
    public IEnumerable<ReviewBasicInfoResponse> Reviews { get; set; } =
        new List<ReviewBasicInfoResponse>();
}

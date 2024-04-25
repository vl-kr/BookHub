using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.DTOs.Responses.Genre;
using BusinessLayer.DTOs.Responses.Publisher;

namespace BusinessLayer.Models;

public class SearchResult
{
    public IEnumerable<BookResponse> Books { get; set; } = new List<BookResponse>();
    public IEnumerable<GenreResponse> Genres { get; set; } = new List<GenreResponse>();
    public IEnumerable<AuthorResponse> Authors { get; set; } = new List<AuthorResponse>();
    public IEnumerable<PublisherResponse> Publishers { get; set; } = new List<PublisherResponse>();
}

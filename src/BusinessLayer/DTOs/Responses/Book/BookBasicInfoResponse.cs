namespace BusinessLayer.DTOs.Responses.Book;

public class BookBasicInfoResponse : BaseResponse
{
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public string? ISBN { get; set; }
    public int YearPublished { get; set; }
    public string? ImageUrl { get; set; }
}

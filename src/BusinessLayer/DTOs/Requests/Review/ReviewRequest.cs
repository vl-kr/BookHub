namespace BusinessLayer.DTOs.Requests.Review;

public class ReviewRequest : BaseRequest
{
    public int Rating { get; set; }
    public string? TextReview { get; set; }
    public int BookId { get; set; }
    public int CustomerId { get; set; }
}

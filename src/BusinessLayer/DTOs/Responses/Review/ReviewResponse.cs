using BusinessLayer.DTOs.Responses.Book;
using BusinessLayer.DTOs.Responses.Customer;

namespace BusinessLayer.DTOs.Responses.Review;

public class ReviewResponse : BaseResponse
{
    public int Rating { get; set; }
    public string? TextReview { get; set; }
    public BookBasicInfoResponse? Book { get; set; }
    public CustomerBasicInfoResponse? Customer { get; set; }
}

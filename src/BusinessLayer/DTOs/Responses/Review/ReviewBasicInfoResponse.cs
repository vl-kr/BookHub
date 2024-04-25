using BusinessLayer.DTOs.Responses.Customer;

namespace BusinessLayer.DTOs.Responses.Review;

public class ReviewBasicInfoResponse : BaseResponse
{
    public int Rating { get; set; }
    public string? TextReview { get; set; }
    public CustomerBasicInfoResponse? Customer { get; set; }
}

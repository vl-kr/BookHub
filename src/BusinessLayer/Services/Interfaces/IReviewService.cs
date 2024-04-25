using BusinessLayer.DTOs.Requests.Review;
using BusinessLayer.DTOs.Responses.Review;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.ReviewFilters;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IReviewService
{
    Task<ServiceResult<ReviewResponse>> CreateReview(ReviewRequest reviewRequest);

    Task<ServiceResult<IEnumerable<ReviewResponse>>> GetReviews(
        PageOptions pageOptions,
        ReviewFilter reviewFilter
    );

    Task<ServiceResult<ReviewResponse>> GetReview(int id);
    Task<ServiceResult<ReviewResponse>> UpdateReview(int id, ReviewRequest reviewRequest);
    Task<ServiceResult<ReviewResponse>> DeleteReview(int id);
}

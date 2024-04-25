using AutoMapper;
using BusinessLayer.DTOs.Requests.Review;
using BusinessLayer.DTOs.Responses.Review;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.ReviewFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class ReviewService : IReviewService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public ReviewService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<ReviewResponse>> CreateReview(ReviewRequest reviewRequest)
    {
        var review = _mapper.Map<Review>(reviewRequest);
        var (isMappingSuccessful, errorMessage) = await MapRelatedEntitiesFromIds(
            review,
            reviewRequest
        );
        if (!isMappingSuccessful)
            return new ServiceResult<ReviewResponse>(errorMessage, ServiceResultCode.Conflict);
        try
        {
            await _uow.ReviewRepository.AddAsync(review);
            await _uow.CommitAsync();
            return new ServiceResult<ReviewResponse>(
                _mapper.Map<ReviewResponse>(review),
                ServiceResultCode.Created
            );
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<ReviewResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<IEnumerable<ReviewResponse>>> GetReviews(
        PageOptions pageOptions,
        ReviewFilter reviewFilter
    )
    {
        var query = new EFCoreQueryObject<Review>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(reviewFilter);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var reviews = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<ReviewResponse>>(
            reviews.Select(_mapper.Map<ReviewResponse>)
        );
    }

    public async Task<ServiceResult<ReviewResponse>> GetReview(int id)
    {
        var review = await _uow.ReviewRepository.FindByIdWithAllRelatedDataAsync(id);
        if (review == null)
            return new ServiceResult<ReviewResponse>(
                "Review not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<ReviewResponse>(_mapper.Map<ReviewResponse>(review));
    }

    public async Task<ServiceResult<ReviewResponse>> UpdateReview(
        int id,
        ReviewRequest reviewRequest
    )
    {
        var existingReview = await _uow.ReviewRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingReview == null)
            return new ServiceResult<ReviewResponse>(
                "Review not found",
                ServiceResultCode.NotFound
            );

        try
        {
            _uow.ReviewRepository.Update(_mapper.Map(reviewRequest, existingReview));
            await _uow.CommitAsync();
            return new ServiceResult<ReviewResponse>(_mapper.Map<ReviewResponse>(existingReview));
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<ReviewResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<ReviewResponse>> DeleteReview(int id)
    {
        var review = await _uow.ReviewRepository.FindByIdAsync(id);
        if (review == null)
            return new ServiceResult<ReviewResponse>(
                "Review not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.ReviewRepository.Remove(review);
            await _uow.CommitAsync();
            return new ServiceResult<ReviewResponse>("", ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<ReviewResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    private async Task<(bool Success, string ErrorMessage)> MapRelatedEntitiesFromIds(
        Review review,
        ReviewRequest reviewRequest
    )
    {
        var customer = await _uow.CustomerRepository.FindByIdAsync(reviewRequest.CustomerId);
        if (customer == null)
            return (false, "A review must belong to a customer.");

        var book = await _uow.BookRepository.FindByIdAsync(reviewRequest.BookId);
        if (book == null)
            return (false, "A review must belong to a book.");

        review.Customer = customer;
        review.Book = book;

        return (true, string.Empty);
    }
}

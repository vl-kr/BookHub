using AutoMapper;
using BusinessLayer.DTOs.Requests.Wishlist;
using BusinessLayer.DTOs.Responses.Wishlist;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class WishlistService : IWishlistService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public WishlistService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<WishlistResponse>> CreateWishlist(
        WishlistRequest wishlistRequest
    )
    {
        var wishlist = _mapper.Map<Wishlist>(wishlistRequest);
        try
        {
            await _uow.WishlistRepository.AddAsync(wishlist);
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<WishlistResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<WishlistResponse>(
            _mapper.Map<WishlistResponse>(wishlist),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<WishlistResponse>>> GetWishlists(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<Wishlist>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var wishlists = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<WishlistResponse>>(
            wishlists.Select(_mapper.Map<WishlistResponse>)
        );
    }

    public async Task<ServiceResult<WishlistResponse>> GetWishlist(int id)
    {
        var wishlist = await _uow.WishlistRepository.FindByIdWithAllRelatedDataAsync(id);
        if (wishlist == null)
            return new ServiceResult<WishlistResponse>(
                "Wishlist not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<WishlistResponse>(_mapper.Map<WishlistResponse>(wishlist));
    }

    public async Task<ServiceResult<WishlistResponse>> UpdateWishlist(
        int id,
        WishlistRequest wishlistRequest
    )
    {
        var existingWishlist = await _uow.WishlistRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingWishlist == null)
            return new ServiceResult<WishlistResponse>(
                "Wishlist not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.WishlistRepository.Update(_mapper.Map(wishlistRequest, existingWishlist));
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<WishlistResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<WishlistResponse>(_mapper.Map<WishlistResponse>(existingWishlist));
    }

    public async Task<ServiceResult<WishlistResponse>> DeleteWishlist(int id)
    {
        var wishlist = await _uow.WishlistRepository.FindByIdWithAllRelatedDataAsync(id);
        if (wishlist == null)
            return new ServiceResult<WishlistResponse>(
                "Wishlist not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.WishlistRepository.Remove(wishlist);
            await _uow.CommitAsync();
            return new ServiceResult<WishlistResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<WishlistResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<Wishlist?> GetWishlistOfCustomer(int customerId)
    {
        var query = new EFCoreQueryObject<Wishlist>(_context);

        query.Include(q => q.IncludeAllRelatedData());

        query.Filter(c => c.CustomerId == customerId);
        var wishlists = await query.ExecuteAsync();

        return wishlists.FirstOrDefault();
    }
}

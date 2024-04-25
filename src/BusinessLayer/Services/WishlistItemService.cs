using AutoMapper;
using BusinessLayer.DTOs.Requests.WishlistItem;
using BusinessLayer.DTOs.Responses.WishlistItem;
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

public class WishlistItemService : IWishlistItemService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public WishlistItemService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<WishlistItemResponse>> CreateWishlistItem(
        WishlistItemRequest wishlistItemRequest
    )
    {
        var wishlistItem = _mapper.Map<WishlistItem>(wishlistItemRequest);
        try
        {
            await _uow.WishlistItemRepository.AddAsync(wishlistItem);
            await _uow.CommitAsync();
            return new ServiceResult<WishlistItemResponse>(
                _mapper.Map<WishlistItemResponse>(wishlistItem),
                ServiceResultCode.Created
            );
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<WishlistItemResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<IEnumerable<WishlistItemResponse>>> GetWishlistItems(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<WishlistItem>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var wishlistItems = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<WishlistItemResponse>>(
            wishlistItems.Select(_mapper.Map<WishlistItemResponse>)
        );
    }

    public async Task<ServiceResult<WishlistItemResponse>> GetWishlistItem(int id)
    {
        var wishlistItem = await _uow.WishlistItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (wishlistItem == null)
            return new ServiceResult<WishlistItemResponse>(
                "WishlistItem not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<WishlistItemResponse>(
            _mapper.Map<WishlistItemResponse>(wishlistItem)
        );
    }

    public async Task<ServiceResult<WishlistItemResponse>> UpdateWishlistItem(
        int id,
        WishlistItemRequest wishlistItemRequest
    )
    {
        var existingWishlistItem =
            await _uow.WishlistItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingWishlistItem == null)
            return new ServiceResult<WishlistItemResponse>(
                "WishlistItem not found",
                ServiceResultCode.NotFound
            );

        try
        {
            _uow.WishlistItemRepository.Update(
                _mapper.Map(wishlistItemRequest, existingWishlistItem)
            );
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<WishlistItemResponse>(ex);
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<WishlistItemResponse>(
            _mapper.Map<WishlistItemResponse>(existingWishlistItem)
        );
    }

    public async Task<ServiceResult<WishlistItemResponse>> DeleteWishlistItem(int id)
    {
        var wishlistItem = await _uow.WishlistItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (wishlistItem == null)
            return new ServiceResult<WishlistItemResponse>(
                "Wishlist item not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.WishlistItemRepository.Remove(wishlistItem);
            await _uow.CommitAsync();
            return new ServiceResult<WishlistItemResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<WishlistItemResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<WishlistItemResponse?>> GetWishlistItemOfBook(
        int id,
        int bookId
    )
    {
        var wishlist = await _uow.WishlistRepository.FindByIdWithAllRelatedDataAsync(id);
        if (wishlist == null)
            return new ServiceResult<WishlistItemResponse?>(
                "Wishlist not found",
                ServiceResultCode.NotFound
            );

        var item = wishlist.WishlistItems.FirstOrDefault(x => x.BookId == bookId);
        if (item == null)
            return new ServiceResult<WishlistItemResponse?>(
                "Wishlist item not found",
                ServiceResultCode.NotFound
            );

        return new ServiceResult<WishlistItemResponse?>(_mapper.Map<WishlistItemResponse>(item));
    }
}

using AutoMapper;
using BusinessLayer.DTOs.Requests.ShoppingCartItem;
using BusinessLayer.DTOs.Responses.ShoppingCartItem;
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

public class ShoppingCartItemService : IShoppingCartItemService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public ShoppingCartItemService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<ShoppingCartItemResponse>> CreateShoppingCartItem(
        ShoppingCartItemRequest shoppingCartItemRequest
    )
    {
        var shoppingCartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemRequest);
        try
        {
            await _uow.ShoppingCartItemRepository.AddAsync(shoppingCartItem);
            await _uow.CommitAsync();
            return new ServiceResult<ShoppingCartItemResponse>(
                _mapper.Map<ShoppingCartItemResponse>(shoppingCartItem),
                ServiceResultCode.Created
            );
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShoppingCartItemResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<IEnumerable<ShoppingCartItemResponse>>> GetShoppingCartItems(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<ShoppingCartItem>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var shoppingCartItems = await query.ExecuteAsync();
        foreach (var cartItem in shoppingCartItems)
            cartItem.TotalPrice = CalculateShoppingCartItemTotalPrice(cartItem);

        return new ServiceResult<IEnumerable<ShoppingCartItemResponse>>(
            shoppingCartItems.Select(_mapper.Map<ShoppingCartItemResponse>)
        );
    }

    public async Task<ServiceResult<ShoppingCartItemResponse>> GetShoppingCartItem(int id)
    {
        var shoppingCartItem =
            await _uow.ShoppingCartItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (shoppingCartItem == null)
            return new ServiceResult<ShoppingCartItemResponse>(
                "ShoppingCartItem not found",
                ServiceResultCode.NotFound
            );

        shoppingCartItem.TotalPrice = CalculateShoppingCartItemTotalPrice(shoppingCartItem);
        return new ServiceResult<ShoppingCartItemResponse>(
            _mapper.Map<ShoppingCartItemResponse>(shoppingCartItem)
        );
    }

    public async Task<ServiceResult<ShoppingCartItemResponse>> UpdateShoppingCartItem(
        int id,
        ShoppingCartItemRequest shoppingCartItemRequest
    )
    {
        var existingShoppingCartItem =
            await _uow.ShoppingCartItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingShoppingCartItem == null)
            return new ServiceResult<ShoppingCartItemResponse>(
                "ShoppingCartItem not found",
                ServiceResultCode.NotFound
            );

        try
        {
            _uow.ShoppingCartItemRepository.Update(
                _mapper.Map(shoppingCartItemRequest, existingShoppingCartItem)
            );
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShoppingCartItemResponse>(ex);
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<ShoppingCartItemResponse>(
            _mapper.Map<ShoppingCartItemResponse>(existingShoppingCartItem)
        );
    }

    public async Task<ServiceResult<ShoppingCartItemResponse>> DeleteShoppingCartItem(int id)
    {
        var shoppingCartItem =
            await _uow.ShoppingCartItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (shoppingCartItem == null)
            return new ServiceResult<ShoppingCartItemResponse>(
                "ShoppingCartItem not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.ShoppingCartItemRepository.Remove(shoppingCartItem);
            await _uow.CommitAsync();
            return new ServiceResult<ShoppingCartItemResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShoppingCartItemResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<ShoppingCartItemResponse>> ChangeQuantity(
        int id,
        int newQuantity
    )
    {
        var shoppingCartItem =
            await _uow.ShoppingCartItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (shoppingCartItem == null)
            return new ServiceResult<ShoppingCartItemResponse>(
                "ShoppingCartItem not found",
                ServiceResultCode.NotFound
            );

        shoppingCartItem.Quantity = newQuantity;
        _uow.ShoppingCartItemRepository.Update(shoppingCartItem);

        await _uow.CommitAsync();
        return new ServiceResult<ShoppingCartItemResponse>(
            _mapper.Map<ShoppingCartItemResponse>(shoppingCartItem)
        );
    }

    public async Task<ServiceResult<ShoppingCartItemResponse?>> GetCartItemOfBook(
        int id,
        int bookId
    )
    {
        var cart = await _uow.ShoppingCartRepository.FindByIdWithAllRelatedDataAsync(id);
        if (cart == null)
            return new ServiceResult<ShoppingCartItemResponse?>(
                "Cart not found",
                ServiceResultCode.NotFound
            );

        var item = cart.ShoppingCartItems.FirstOrDefault(x => x.BookId == bookId);
        if (item == null)
            return new ServiceResult<ShoppingCartItemResponse?>(
                "Wishlist item not found",
                ServiceResultCode.NotFound
            );

        return new ServiceResult<ShoppingCartItemResponse?>(
            _mapper.Map<ShoppingCartItemResponse>(item)
        );
    }

    private static decimal CalculateShoppingCartItemTotalPrice(ShoppingCartItem cartItem)
    {
        if (cartItem.Book == null)
            return 0;
        return cartItem.Book.Price * cartItem.Quantity;
    }
}

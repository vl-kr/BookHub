using AutoMapper;
using BusinessLayer.DTOs.Requests.ShoppingCart;
using BusinessLayer.DTOs.Responses.ShoppingCart;
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

public class ShoppingCartService : IShoppingCartService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public ShoppingCartService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<ShoppingCartResponse>> CreateShoppingCart(
        ShoppingCartRequest shoppingCartRequest
    )
    {
        var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartRequest);
        try
        {
            await _uow.ShoppingCartRepository.AddAsync(shoppingCart);
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShoppingCartResponse>(ex);
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<ShoppingCartResponse>(
            _mapper.Map<ShoppingCartResponse>(shoppingCart),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<ShoppingCartResponse>>> GetShoppingCarts(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<ShoppingCart>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var shoppingCarts = await query.ExecuteAsync();
        foreach (var shoppingCart in shoppingCarts)
            shoppingCart.TotalPrice = CalculateShoppingCartTotalPrice(shoppingCart);
        return new ServiceResult<IEnumerable<ShoppingCartResponse>>(
            shoppingCarts.Select(_mapper.Map<ShoppingCartResponse>)
        );
    }

    public async Task<ServiceResult<ShoppingCartResponse>> GetShoppingCart(int id)
    {
        var shoppingCart = await _uow.ShoppingCartRepository.FindByIdWithAllRelatedDataAsync(id);
        if (shoppingCart == null)
            return new ServiceResult<ShoppingCartResponse>(
                "Shopping cart not found",
                ServiceResultCode.NotFound
            );
        shoppingCart.TotalPrice = CalculateShoppingCartTotalPrice(shoppingCart);
        return new ServiceResult<ShoppingCartResponse>(
            _mapper.Map<ShoppingCartResponse>(shoppingCart)
        );
    }

    public async Task<ServiceResult<ShoppingCartResponse>> UpdateShoppingCart(
        int id,
        ShoppingCartRequest shoppingCartRequest
    )
    {
        var existingShoppingCart =
            await _uow.ShoppingCartRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingShoppingCart == null)
            return new ServiceResult<ShoppingCartResponse>(
                "Shopping cart not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.ShoppingCartRepository.Update(
                _mapper.Map(shoppingCartRequest, existingShoppingCart)
            );
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShoppingCartResponse>(ex);
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<ShoppingCartResponse>(
            _mapper.Map<ShoppingCartResponse>(existingShoppingCart)
        );
    }

    public async Task<ServiceResult<ShoppingCartResponse>> DeleteShoppingCart(int id)
    {
        var shoppingCart = await _uow.ShoppingCartRepository.FindByIdWithAllRelatedDataAsync(id);
        if (shoppingCart == null)
            return new ServiceResult<ShoppingCartResponse>(
                "Shopping cart not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.ShoppingCartRepository.Remove(shoppingCart);
            await _uow.CommitAsync();
            return new ServiceResult<ShoppingCartResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShoppingCartResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ShoppingCart?> GetShoppingCartOfCustomer(int customerId)
    {
        var query = new EFCoreQueryObject<ShoppingCart>(_context);

        query.Include(q => q.IncludeAllRelatedData());
        query.Filter(c => c.CustomerId == customerId);

        var shoppingCarts = await query.ExecuteAsync();
        var shoppingCart = shoppingCarts.FirstOrDefault();
        if (shoppingCart == null)
            return null;

        shoppingCart.TotalPrice = CalculateShoppingCartTotalPrice(shoppingCart);
        return shoppingCart;
    }

    private static decimal CalculateShoppingCartTotalPrice(ShoppingCart cart)
    {
        foreach (var cartItem in cart.ShoppingCartItems)
        {
            if (cartItem.Book == null)
                return 0;
            cartItem.TotalPrice = cartItem.Quantity * cartItem.Book.Price;
        }

        return cart.ShoppingCartItems.Sum(x => x.TotalPrice);
    }
}

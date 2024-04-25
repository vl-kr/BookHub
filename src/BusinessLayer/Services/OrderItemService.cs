using AutoMapper;
using BusinessLayer.DTOs.Requests.OrderItem;
using BusinessLayer.DTOs.Responses.OrderItem;
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

public class OrderItemService : IOrderItemService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public OrderItemService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<OrderItemResponse>> CreateOrderItem(
        OrderItemRequest orderItemRequest
    )
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemRequest);
        try
        {
            var (isMappingSuccessful, errorMessage) = await MapRelatedEntitiesFromIds(
                orderItem,
                orderItemRequest
            );
            if (!isMappingSuccessful)
                return new ServiceResult<OrderItemResponse>(
                    errorMessage,
                    ServiceResultCode.Conflict
                );

            orderItem.TotalPrice = CalculateOrderItemTotalPrice(orderItem);
            await _uow.OrderItemRepository.AddAsync(orderItem);
            await _uow.CommitAsync();
            return new ServiceResult<OrderItemResponse>(
                _mapper.Map<OrderItemResponse>(orderItem),
                ServiceResultCode.Created
            );
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<OrderItemResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<IEnumerable<OrderItemResponse>>> GetOrderItems(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<OrderItem>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var orderItems = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<OrderItemResponse>>(
            orderItems.Select(_mapper.Map<OrderItemResponse>)
        );
    }

    public async Task<ServiceResult<OrderItemResponse>> GetOrderItem(int id)
    {
        var orderItem = await _uow.OrderItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (orderItem == null)
            return new ServiceResult<OrderItemResponse>(
                "OrderItem not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<OrderItemResponse>(_mapper.Map<OrderItemResponse>(orderItem));
    }

    public async Task<ServiceResult<OrderItemResponse>> UpdateOrderItem(
        int id,
        OrderItemRequest orderItemRequest
    )
    {
        var existingOrderItem = await _uow.OrderItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingOrderItem == null)
            return new ServiceResult<OrderItemResponse>(
                "OrderItem not found",
                ServiceResultCode.NotFound
            );

        try
        {
            _uow.OrderItemRepository.Update(_mapper.Map(orderItemRequest, existingOrderItem));
            await _uow.CommitAsync();
            return new ServiceResult<OrderItemResponse>(
                _mapper.Map<OrderItemResponse>(existingOrderItem)
            );
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<OrderItemResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<OrderItemResponse>> DeleteOrderItem(int id)
    {
        var orderItem = await _uow.OrderItemRepository.FindByIdWithAllRelatedDataAsync(id);
        if (orderItem == null)
            return new ServiceResult<OrderItemResponse>(
                "OrderItem not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.OrderItemRepository.Remove(orderItem);
            await _uow.CommitAsync();
            return new ServiceResult<OrderItemResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<OrderItemResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    private async Task<(bool Success, string ErrorMessage)> MapRelatedEntitiesFromIds(
        OrderItem orderItem,
        OrderItemRequest orderItemRequest
    )
    {
        var book = await _uow.BookRepository.FindByIdAsync(orderItemRequest.BookId);
        if (book == null)
            return (false, "An order item must have a book.");

        orderItem.Book = book;
        return (true, string.Empty);
    }

    private static decimal CalculateOrderItemTotalPrice(OrderItem orderItem)
    {
        if (orderItem.Book == null)
            return 0;
        return orderItem.Quantity * orderItem.Book.Price;
    }
}

using AutoMapper;
using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.DTOs.Responses.Order;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.OrderFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class OrderService : IOrderService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public OrderService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<OrderResponse>> CreateOrder(OrderRequest orderRequest)
    {
        var order = _mapper.Map<Order>(orderRequest);
        try
        {
            var (isMappingSuccessful, errorMessage) = await MapRelatedEntitiesFromIds(
                order,
                orderRequest
            );
            if (!isMappingSuccessful)
                return new ServiceResult<OrderResponse>(errorMessage, ServiceResultCode.Conflict);

            order.TotalPrice = CalculateOrderTotalPrice(order);
            await _uow.OrderRepository.AddAsync(order);
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<OrderResponse>(ex);
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<OrderResponse>(
            _mapper.Map<OrderResponse>(order),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<OrderResponse>>> GetOrders(
        PageOptions pageOptions,
        OrderFilter orderFilter
    )
    {
        var query = new EFCoreQueryObject<Order>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(orderFilter);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var orders = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<OrderResponse>>(
            orders.Select(_mapper.Map<OrderResponse>)
        );
    }

    public async Task<ServiceResult<OrderResponse>> GetOrder(int id)
    {
        var order = await _uow.OrderRepository.FindByIdWithAllRelatedDataAsync(id);
        if (order == null)
            return new ServiceResult<OrderResponse>("Order not found", ServiceResultCode.NotFound);
        return new ServiceResult<OrderResponse>(_mapper.Map<OrderResponse>(order));
    }

    public async Task<ServiceResult<OrderResponse>> UpdateOrder(
        int id,
        OrderEditRequest orderEditDto
    )
    {
        var existingOrder = await _uow.OrderRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingOrder == null)
            return new ServiceResult<OrderResponse>("Order not found", ServiceResultCode.NotFound);
        try
        {
            var a = _mapper.Map(orderEditDto, existingOrder);
            _uow.OrderRepository.Update(a);
            await _uow.CommitAsync();
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<OrderResponse>(ex);
            if (response != null)
                return response;
            throw;
        }

        return new ServiceResult<OrderResponse>(_mapper.Map<OrderResponse>(existingOrder));
    }

    public async Task<ServiceResult<OrderResponse>> DeleteOrder(int id)
    {
        var order = await _uow.OrderRepository.FindByIdWithAllRelatedDataAsync(id);
        if (order == null)
            return new ServiceResult<OrderResponse>("Order not found", ServiceResultCode.NotFound);
        try
        {
            _uow.OrderRepository.Remove(order);
            await _uow.CommitAsync();
            return new ServiceResult<OrderResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<OrderResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<PaginationObject<Order>> GetPaginatedOrders(
        PageOptions pageOptions,
        OrderFilter orderFilter
    )
    {
        var query = new EFCoreQueryObject<Order>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(orderFilter);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);

        var result = await query.GetPagedResultAsync(
            pageOptions.Page ?? 1,
            pageOptions.PageSize ?? 10
        );

        return result;
    }

    public async Task<PaginationObject<Order>> GetPaginatedOrdersOfCustomer(
        int customerId,
        PageOptions pageOptions,
        OrderFilter orderFilter
    )
    {
        var query = new EFCoreQueryObject<Order>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.Filter(o => o.CustomerId == customerId);
        query.FilterOn(orderFilter);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);

        var result = await query.GetPagedResultAsync(
            pageOptions.Page ?? 1,
            pageOptions.PageSize ?? 10
        );
        return result;
    }

    private async Task<(bool Success, string ErrorMessage)> MapRelatedEntitiesFromIds(
        Order order,
        OrderRequest orderRequest
    )
    {
        var orderItems = await _uow.OrderItemRepository.FilterAsync(g =>
            orderRequest.OrderItemIds.Contains(g.Id)
        );
        if (!orderItems.Any())
            return (false, "An order must have some orderItems (created in advance).");

        order.OrderItems = orderItems.ToList();

        return (true, string.Empty);
    }

    private static decimal CalculateOrderTotalPrice(Order order)
    {
        return order.OrderItems.Sum(x => x.TotalPrice);
    }
}

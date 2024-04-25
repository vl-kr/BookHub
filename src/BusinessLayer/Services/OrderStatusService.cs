using AutoMapper;
using BusinessLayer.DTOs.Requests.OrderStatus;
using BusinessLayer.DTOs.Responses.OrderStatus;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class OrderStatusService : IOrderStatusService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public OrderStatusService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<OrderStatusResponse>> CreateOrderStatus(
        OrderStatusRequest orderStatusRequest
    )
    {
        var orderStatus = _mapper.Map<OrderStatus>(orderStatusRequest);
        await _uow.OrderStatusRepository.AddAsync(orderStatus);
        await _uow.CommitAsync();
        return new ServiceResult<OrderStatusResponse>(
            _mapper.Map<OrderStatusResponse>(orderStatus),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<OrderStatusResponse>>> GetOrderStatuses(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<OrderStatus>(_context);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var orderStatuses = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<OrderStatusResponse>>(
            orderStatuses.Select(_mapper.Map<OrderStatusResponse>)
        );
    }

    public async Task<ServiceResult<OrderStatusResponse>> GetOrderStatus(int id)
    {
        var orderStatus = await _uow.OrderStatusRepository.FindByIdAsync(id);
        if (orderStatus == null)
            return new ServiceResult<OrderStatusResponse>(
                "OrderStatus not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<OrderStatusResponse>(
            _mapper.Map<OrderStatusResponse>(orderStatus)
        );
    }

    public async Task<ServiceResult<OrderStatusResponse>> UpdateOrderStatus(
        int id,
        OrderStatusRequest orderStatusRequest
    )
    {
        var existingOrderStatus = await _uow.OrderStatusRepository.FindByIdAsync(id);
        if (existingOrderStatus == null)
            return new ServiceResult<OrderStatusResponse>(
                "OrderStatus not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.OrderStatusRepository.Update(_mapper.Map(orderStatusRequest, existingOrderStatus));
            await _uow.CommitAsync();
            return new ServiceResult<OrderStatusResponse>(
                _mapper.Map<OrderStatusResponse>(existingOrderStatus)
            );
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<OrderStatusResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<OrderStatusResponse>> DeleteOrderStatus(int id)
    {
        var orderStatus = await _uow.OrderStatusRepository.FindByIdAsync(id);
        if (orderStatus == null)
            return new ServiceResult<OrderStatusResponse>(
                "OrderStatus not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.OrderStatusRepository.Remove(orderStatus);
            await _uow.CommitAsync();
            return new ServiceResult<OrderStatusResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<OrderStatusResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }
}

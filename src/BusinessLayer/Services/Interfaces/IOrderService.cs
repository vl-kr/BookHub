using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.DTOs.Responses.Order;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.OrderFilters;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderService
{
    Task<ServiceResult<OrderResponse>> CreateOrder(OrderRequest orderRequest);

    Task<ServiceResult<IEnumerable<OrderResponse>>> GetOrders(
        PageOptions pageOptions,
        OrderFilter orderFilter
    );

    Task<ServiceResult<OrderResponse>> GetOrder(int id);
    Task<ServiceResult<OrderResponse>> UpdateOrder(int id, OrderEditRequest orderEditDto);
    Task<ServiceResult<OrderResponse>> DeleteOrder(int id);

    Task<PaginationObject<Order>> GetPaginatedOrdersOfCustomer(
        int customerId,
        PageOptions pageOptions,
        OrderFilter orderFilter
    );

    Task<PaginationObject<Order>> GetPaginatedOrders(
        PageOptions pageOptions,
        OrderFilter orderFilter
    );
}

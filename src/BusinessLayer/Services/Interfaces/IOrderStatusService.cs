using BusinessLayer.DTOs.Requests.OrderStatus;
using BusinessLayer.DTOs.Responses.OrderStatus;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderStatusService
{
    Task<ServiceResult<OrderStatusResponse>> CreateOrderStatus(
        OrderStatusRequest orderStatusRequest
    );

    Task<ServiceResult<IEnumerable<OrderStatusResponse>>> GetOrderStatuses(PageOptions pageOptions);
    Task<ServiceResult<OrderStatusResponse>> GetOrderStatus(int id);

    Task<ServiceResult<OrderStatusResponse>> UpdateOrderStatus(
        int id,
        OrderStatusRequest orderStatusRequest
    );

    Task<ServiceResult<OrderStatusResponse>> DeleteOrderStatus(int id);
}

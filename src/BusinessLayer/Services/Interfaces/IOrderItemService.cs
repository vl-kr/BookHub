using BusinessLayer.DTOs.Requests.OrderItem;
using BusinessLayer.DTOs.Responses.OrderItem;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderItemService
{
    Task<ServiceResult<OrderItemResponse>> CreateOrderItem(OrderItemRequest orderItemRequest);
    Task<ServiceResult<IEnumerable<OrderItemResponse>>> GetOrderItems(PageOptions pageOptions);
    Task<ServiceResult<OrderItemResponse>> GetOrderItem(int id);

    Task<ServiceResult<OrderItemResponse>> UpdateOrderItem(
        int id,
        OrderItemRequest orderItemRequest
    );

    Task<ServiceResult<OrderItemResponse>> DeleteOrderItem(int id);
}

using BusinessLayer.DTOs.Responses.Order;

namespace BusinessLayer.Coordinators.Interfaces;

public interface ICartToOrderCoordinator
{
    Task<OrderResponse?> CreateOrderFromCartAsync(int cartId);
}

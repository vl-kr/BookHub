using AutoMapper;
using BusinessLayer.Coordinators.Interfaces;
using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.DTOs.Requests.OrderItem;
using BusinessLayer.DTOs.Responses.Order;
using BusinessLayer.Enums;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.UnitOfWork;

namespace BusinessLayer.Coordinators;

public class CartToOrderCoordinator : ICartToOrderCoordinator
{
    private readonly IMapper _mapper;
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderService _orderService;
    private readonly IShoppingCartItemService _shoppingCartItemService;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IUnitOfWork _unitOfWork;

    public CartToOrderCoordinator(
        IShoppingCartService shoppingCartService,
        IShoppingCartItemService shoppingCartItemService,
        IOrderItemService orderItemService,
        IOrderService orderService,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _shoppingCartItemService = shoppingCartItemService;
        _shoppingCartService = shoppingCartService;
        _orderItemService = orderItemService;
        _orderService = orderService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderResponse?> CreateOrderFromCartAsync(int cartId)
    {
        var cartResult = await _shoppingCartService.GetShoppingCart(cartId);
        if (cartResult.StatusCode != ServiceResultCode.OK || cartResult.Data?.Customer == null)
            return null;

        var cart = cartResult.Data;
        var orderItemsIds = new List<int>();

        await using var transaction = _unitOfWork.BeginTransaction();

        foreach (var item in cart.ShoppingCartItems)
        {
            var orderItem = await _orderItemService.CreateOrderItem(
                _mapper.Map<OrderItemRequest>(item)
            );
            if (orderItem.StatusCode != ServiceResultCode.Created || orderItem.Data == null)
            {
                await transaction.RollbackAsync();
                return null;
            }

            orderItemsIds.Add(orderItem.Data.Id);

            var deleteResult = await _shoppingCartItemService.DeleteShoppingCartItem(item.Id);
            if (deleteResult.StatusCode != ServiceResultCode.NoContent)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        var orderRequest = new OrderRequest
        {
            CustomerId = cart.Customer.Id,
            OrderStatusId = 1,
            OrderItemIds = orderItemsIds
        };

        var orderResult = await _orderService.CreateOrder(orderRequest);
        if (orderResult.StatusCode != ServiceResultCode.Created)
        {
            await transaction.RollbackAsync();
            return null;
        }

        await transaction.CommitAsync();
        return orderResult.Data;
    }
}

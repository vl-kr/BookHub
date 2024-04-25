using BusinessLayer.DTOs.Requests.OrderItem;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrderItemController : BaseController
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _orderItemService = orderItemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderItemRequest newOrderItem)
    {
        var serviceResult = await _orderItemService.CreateOrderItem(newOrderItem);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _orderItemService.GetOrderItems(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _orderItemService.GetOrderItem(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderItemRequest updatedOrderItem)
    {
        var serviceResult = await _orderItemService.UpdateOrderItem(id, updatedOrderItem);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _orderItemService.DeleteOrderItem(id);
        return BuildResponse(serviceResult);
    }
}

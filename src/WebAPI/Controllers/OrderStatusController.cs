using BusinessLayer.DTOs.Requests.OrderStatus;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrderStatusController : BaseController
{
    private readonly IOrderStatusService _orderStatusService;

    public OrderStatusController(IOrderStatusService orderStatusService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _orderStatusService = orderStatusService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderStatusRequest newOrderStatus)
    {
        var serviceResult = await _orderStatusService.CreateOrderStatus(newOrderStatus);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _orderStatusService.GetOrderStatuses(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _orderStatusService.GetOrderStatus(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] OrderStatusRequest updatedOrderStatus
    )
    {
        var serviceResult = await _orderStatusService.UpdateOrderStatus(id, updatedOrderStatus);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _orderStatusService.DeleteOrderStatus(id);
        return BuildResponse(serviceResult);
    }
}

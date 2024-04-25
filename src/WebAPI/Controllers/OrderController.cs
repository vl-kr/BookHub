using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.OrderFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrderController : BaseController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderRequest newOrder)
    {
        var serviceResult = await _orderService.CreateOrder(newOrder);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PageOptions pageOptions,
        [FromQuery] OrderFilter orderFilter
    )
    {
        var serviceResult = await _orderService.GetOrders(pageOptions, orderFilter);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _orderService.GetOrder(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderEditRequest updatedOrder)
    {
        var serviceResult = await _orderService.UpdateOrder(id, updatedOrder);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _orderService.DeleteOrder(id);
        return BuildResponse(serviceResult);
    }
}

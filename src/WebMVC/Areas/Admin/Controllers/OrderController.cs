using AutoMapper;
using BusinessLayer.DTOs.Requests.Order;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.OrderFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Areas.Admin.Models.Order;

namespace WebMVC.Areas.Admin.Controllers;

public class OrderController : AdminController
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService, IMapper mapper, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(
        [FromQuery] PageOptions options,
        [FromQuery] OrderFilter orderFilter
    )
    {
        var orders = await _orderService.GetPaginatedOrders(options, orderFilter);
        return View("~/Views/Order/Index.cshtml", orders);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var orderResult = await _orderService.GetOrder(id);
        var handleResult = HandleReadResult(orderResult);
        if (handleResult != null)
            return handleResult;

        var order = orderResult.Data;

        return View(_mapper.Map<OrderUpdateViewModel>(order));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, OrderUpdateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var orderResult = await _orderService.UpdateOrder(id, _mapper.Map<OrderEditRequest>(model));
        var handleResult = HandleEditResult(orderResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(
            nameof(Index),
            nameof(OrderController).Replace("Controller", ""),
            new { Area = "" }
        );
    }

    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _orderService.DeleteOrder(id);
        var handleResult = HandleDeleteResult(deleteResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(
            nameof(Index),
            nameof(OrderController).Replace("Controller", ""),
            new { Area = "" }
        );
    }
}

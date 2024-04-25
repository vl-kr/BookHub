using System.Net;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.OrderFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models.Order;

namespace WebMVC.Controllers;

[Authorize]
public class OrderController : BaseController
{
    private readonly IAuthService _authService;
    private readonly ILocalIdentityUserService _localIdentityUserService;
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;

    public OrderController(
        IOrderService orderService,
        IAuthService authService,
        IMapper mapper,
        ILocalIdentityUserService localIdentityUserService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _orderService = orderService;
        _authService = authService;
        _mapper = mapper;
        _localIdentityUserService = localIdentityUserService;
    }

    public async Task<IActionResult> Index(
        [FromQuery] PageOptions options,
        [FromQuery] OrderFilter orderFilter
    )
    {
        var user = await _authService.GetUserAsync(User);
        if (user == null)
            return HandleError("User not found", HttpStatusCode.InternalServerError);

        var userResult = await _localIdentityUserService.GetLocalIdentityUser(user.Id);
        var handleResult = HandleReadResult(userResult);
        if (handleResult != null)
            return handleResult;

        var orders = await _orderService.GetPaginatedOrdersOfCustomer(
            userResult.Data.Customer.Id,
            options,
            orderFilter
        );
        return View(orders);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var orderResult = await _orderService.GetOrder(id);
        var handleResult = HandleReadResult(orderResult);
        if (handleResult != null)
            return handleResult;

        var order = _mapper.Map<OrderDetailViewModel>(orderResult.Data);
        return View(order);
    }
}

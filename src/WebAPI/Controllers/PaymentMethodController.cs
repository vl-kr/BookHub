using BusinessLayer.DTOs.Requests.PaymentMethod;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PaymentMethodController : BaseController
{
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodController(
        IPaymentMethodService paymentMethodService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _paymentMethodService = paymentMethodService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(PaymentMethodRequest newPaymentMethod)
    {
        var serviceResult = await _paymentMethodService.CreatePaymentMethod(newPaymentMethod);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _paymentMethodService.GetPaymentMethods(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _paymentMethodService.GetPaymentMethod(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] PaymentMethodRequest updatedPaymentMethod
    )
    {
        var serviceResult = await _paymentMethodService.UpdatePaymentMethod(
            id,
            updatedPaymentMethod
        );
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _paymentMethodService.DeletePaymentMethod(id);
        return BuildResponse(serviceResult);
    }
}

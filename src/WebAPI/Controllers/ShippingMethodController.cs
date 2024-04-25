using BusinessLayer.DTOs.Requests.ShippingMethod;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ShippingMethodController : BaseController
{
    private readonly IShippingMethodService _shippingMethodService;

    public ShippingMethodController(
        IShippingMethodService shippingMethodService,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _shippingMethodService = shippingMethodService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ShippingMethodRequest newShippingMethod)
    {
        var serviceResult = await _shippingMethodService.CreateShippingMethod(newShippingMethod);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _shippingMethodService.GetShippingMethods(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _shippingMethodService.GetShippingMethod(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] ShippingMethodRequest updatedShippingMethod
    )
    {
        var serviceResult = await _shippingMethodService.UpdateShippingMethod(
            id,
            updatedShippingMethod
        );
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _shippingMethodService.DeleteShippingMethod(id);
        return BuildResponse(serviceResult);
    }
}

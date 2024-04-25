using BusinessLayer.DTOs.Requests.Customer;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerRequest newCustomer)
    {
        var serviceResult = await _customerService.CreateCustomer(newCustomer);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageOptions pageOptions)
    {
        var serviceResult = await _customerService.GetCustomers(pageOptions);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _customerService.GetCustomer(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest updatedCustomer)
    {
        var serviceResult = await _customerService.UpdateCustomer(id, updatedCustomer);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _customerService.DeleteCustomer(id);
        return BuildResponse(serviceResult);
    }
}

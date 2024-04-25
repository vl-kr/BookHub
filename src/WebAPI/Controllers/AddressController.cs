using BusinessLayer.DTOs.Requests.Address;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.AddressFilters;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AddressController : BaseController
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _addressService = addressService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddressRequest newAddress)
    {
        var serviceResult = await _addressService.CreateAddress(newAddress);
        return BuildResponse(serviceResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PageOptions pageOptions,
        [FromQuery] AddressFilter addressFilter
    )
    {
        var serviceResult = await _addressService.GetAddresses(pageOptions, addressFilter);
        return BuildResponse(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var serviceResult = await _addressService.GetAddress(id);
        return BuildResponse(serviceResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AddressRequest updatedAddress)
    {
        var serviceResult = await _addressService.UpdateAddress(id, updatedAddress);
        return BuildResponse(serviceResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var serviceResult = await _addressService.DeleteAddress(id);
        return BuildResponse(serviceResult);
    }
}

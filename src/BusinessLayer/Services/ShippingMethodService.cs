using AutoMapper;
using BusinessLayer.DTOs.Requests.ShippingMethod;
using BusinessLayer.DTOs.Responses.ShippingMethod;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class ShippingMethodService : IShippingMethodService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public ShippingMethodService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<ShippingMethodResponse>> CreateShippingMethod(
        ShippingMethodRequest shippingMethodRequest
    )
    {
        var shippingMethod = _mapper.Map<ShippingMethod>(shippingMethodRequest);
        await _uow.ShippingMethodRepository.AddAsync(shippingMethod);
        await _uow.CommitAsync();
        return new ServiceResult<ShippingMethodResponse>(
            _mapper.Map<ShippingMethodResponse>(shippingMethod),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<ShippingMethodResponse>>> GetShippingMethods(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<ShippingMethod>(_context);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var shippingMethods = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<ShippingMethodResponse>>(
            shippingMethods.Select(_mapper.Map<ShippingMethodResponse>)
        );
    }

    public async Task<ServiceResult<ShippingMethodResponse>> GetShippingMethod(int id)
    {
        var shippingMethod = await _uow.ShippingMethodRepository.FindByIdAsync(id);
        if (shippingMethod == null)
            return new ServiceResult<ShippingMethodResponse>(
                "Shipping method not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<ShippingMethodResponse>(
            _mapper.Map<ShippingMethodResponse>(shippingMethod)
        );
    }

    public async Task<ServiceResult<ShippingMethodResponse>> UpdateShippingMethod(
        int id,
        ShippingMethodRequest shippingMethodRequest
    )
    {
        var existingShippingMethod = await _uow.ShippingMethodRepository.FindByIdAsync(id);
        if (existingShippingMethod == null)
            return new ServiceResult<ShippingMethodResponse>(
                "Shipping method not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.ShippingMethodRepository.Update(
                _mapper.Map(shippingMethodRequest, existingShippingMethod)
            );
            await _uow.CommitAsync();
            return new ServiceResult<ShippingMethodResponse>(
                _mapper.Map<ShippingMethodResponse>(existingShippingMethod)
            );
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShippingMethodResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<ShippingMethodResponse>> DeleteShippingMethod(int id)
    {
        var shippingMethod = await _uow.ShippingMethodRepository.FindByIdAsync(id);
        if (shippingMethod == null)
            return new ServiceResult<ShippingMethodResponse>(
                "Shipping method not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.ShippingMethodRepository.Remove(shippingMethod);
            await _uow.CommitAsync();
            return new ServiceResult<ShippingMethodResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<ShippingMethodResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }
}

using AutoMapper;
using BusinessLayer.DTOs.Requests.Address;
using BusinessLayer.DTOs.Responses.Address;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.AddressFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class AddressService : IAddressService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public AddressService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<AddressResponse>> CreateAddress(AddressRequest addressRequest)
    {
        var address = _mapper.Map<Address>(addressRequest);
        await _uow.AddressRepository.AddAsync(address);
        await _uow.CommitAsync();
        return new ServiceResult<AddressResponse>(
            _mapper.Map<AddressResponse>(address),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<AddressResponse>>> GetAddresses(
        PageOptions pageOptions,
        AddressFilter addressFilter
    )
    {
        var query = new EFCoreQueryObject<Address>(_context);

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(addressFilter);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var addresses = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<AddressResponse>>(
            addresses.Select(_mapper.Map<AddressResponse>)
        );
    }

    public async Task<ServiceResult<AddressResponse>> GetAddress(int id)
    {
        var address = await _uow.AddressRepository.FindByIdAsync(id);
        if (address == null)
            return new ServiceResult<AddressResponse>(
                "Address not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<AddressResponse>(_mapper.Map<AddressResponse>(address));
    }

    public async Task<ServiceResult<AddressResponse>> UpdateAddress(
        int id,
        AddressRequest addressRequest
    )
    {
        var existingAddress = await _uow.AddressRepository.FindByIdAsync(id);
        if (existingAddress == null)
            return new ServiceResult<AddressResponse>(
                "Address not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.AddressRepository.Update(_mapper.Map(addressRequest, existingAddress));
            await _uow.CommitAsync();
            return new ServiceResult<AddressResponse>(
                _mapper.Map<AddressResponse>(existingAddress)
            );
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<AddressResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<AddressResponse>> DeleteAddress(int id)
    {
        var address = await _uow.AddressRepository.FindByIdAsync(id);
        if (address == null)
            return new ServiceResult<AddressResponse>(
                "Address not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.AddressRepository.Remove(address);
            await _uow.CommitAsync();
            return new ServiceResult<AddressResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<AddressResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }
}

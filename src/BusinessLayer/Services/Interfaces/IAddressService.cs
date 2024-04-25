using BusinessLayer.DTOs.Requests.Address;
using BusinessLayer.DTOs.Responses.Address;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.AddressFilters;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IAddressService
{
    Task<ServiceResult<AddressResponse>> CreateAddress(AddressRequest addressRequest);

    Task<ServiceResult<IEnumerable<AddressResponse>>> GetAddresses(
        PageOptions pageOptions,
        AddressFilter addressFilter
    );

    Task<ServiceResult<AddressResponse>> GetAddress(int id);
    Task<ServiceResult<AddressResponse>> UpdateAddress(int id, AddressRequest addressRequest);
    Task<ServiceResult<AddressResponse>> DeleteAddress(int id);
}

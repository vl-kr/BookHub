using BusinessLayer.DTOs.Requests.ShippingMethod;
using BusinessLayer.DTOs.Responses.ShippingMethod;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IShippingMethodService
{
    Task<ServiceResult<ShippingMethodResponse>> CreateShippingMethod(
        ShippingMethodRequest shippingMethodRequest
    );

    Task<ServiceResult<IEnumerable<ShippingMethodResponse>>> GetShippingMethods(
        PageOptions pageOptions
    );

    Task<ServiceResult<ShippingMethodResponse>> GetShippingMethod(int id);

    Task<ServiceResult<ShippingMethodResponse>> UpdateShippingMethod(
        int id,
        ShippingMethodRequest shippingMethodRequest
    );

    Task<ServiceResult<ShippingMethodResponse>> DeleteShippingMethod(int id);
}

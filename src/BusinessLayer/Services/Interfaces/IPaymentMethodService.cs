using BusinessLayer.DTOs.Requests.PaymentMethod;
using BusinessLayer.DTOs.Responses.PaymentMethod;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface IPaymentMethodService
{
    Task<ServiceResult<PaymentMethodResponse>> CreatePaymentMethod(
        PaymentMethodRequest paymentMethodRequest
    );

    Task<ServiceResult<IEnumerable<PaymentMethodResponse>>> GetPaymentMethods(
        PageOptions pageOptions
    );

    Task<ServiceResult<PaymentMethodResponse>> GetPaymentMethod(int id);

    Task<ServiceResult<PaymentMethodResponse>> UpdatePaymentMethod(
        int id,
        PaymentMethodRequest paymentMethodRequest
    );

    Task<ServiceResult<PaymentMethodResponse>> DeletePaymentMethod(int id);
}

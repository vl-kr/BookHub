using BusinessLayer.DTOs.Requests.Customer;
using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface ICustomerService
{
    Task<ServiceResult<CustomerResponse>> CreateCustomer(CustomerRequest customerRequest);
    Task<ServiceResult<IEnumerable<CustomerResponse>>> GetCustomers(PageOptions pageOptions);
    Task<ServiceResult<CustomerResponse>> GetCustomer(int id);
    Task<ServiceResult<CustomerResponse>> UpdateCustomer(int id, CustomerRequest customerRequest);
    Task<ServiceResult<CustomerResponse>> DeleteCustomer(int id);
}

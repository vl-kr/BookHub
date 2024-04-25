using AutoMapper;
using BusinessLayer.DTOs.Requests.Customer;
using BusinessLayer.DTOs.Responses.Customer;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class CustomerService : ICustomerService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public CustomerService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<CustomerResponse>> CreateCustomer(
        CustomerRequest customerRequest
    )
    {
        var customer = _mapper.Map<Customer>(customerRequest);
        var (isMappingSuccessful, errorMessage) = await MapRelatedEntitiesFromIds(
            customer,
            customerRequest
        );
        if (!isMappingSuccessful)
            return new ServiceResult<CustomerResponse>(errorMessage, ServiceResultCode.Conflict);
        try
        {
            await _uow.CustomerRepository.AddAsync(customer);
            await _uow.CommitAsync();
            return new ServiceResult<CustomerResponse>(
                _mapper.Map<CustomerResponse>(customer),
                ServiceResultCode.Created
            );
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<CustomerResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<IEnumerable<CustomerResponse>>> GetCustomers(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<Customer>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var customers = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<CustomerResponse>>(
            customers.Select(_mapper.Map<CustomerResponse>)
        );
    }

    public async Task<ServiceResult<CustomerResponse>> GetCustomer(int id)
    {
        var customer = await _uow.CustomerRepository.FindByIdWithAllRelatedDataAsync(id);
        if (customer == null)
            return new ServiceResult<CustomerResponse>(
                "Customer not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<CustomerResponse>(_mapper.Map<CustomerResponse>(customer));
    }

    public async Task<ServiceResult<CustomerResponse>> UpdateCustomer(
        int id,
        CustomerRequest customerRequest
    )
    {
        var existingCustomer = await _uow.CustomerRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingCustomer == null)
            return new ServiceResult<CustomerResponse>(
                "Customer not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.CustomerRepository.Update(_mapper.Map(customerRequest, existingCustomer));
            await _uow.CommitAsync();
            return new ServiceResult<CustomerResponse>(
                _mapper.Map<CustomerResponse>(existingCustomer)
            );
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<CustomerResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<CustomerResponse>> DeleteCustomer(int id)
    {
        var customer = await _uow.CustomerRepository.FindByIdWithAllRelatedDataAsync(id);
        if (customer == null)
            return new ServiceResult<CustomerResponse>(
                "Customer not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.CustomerRepository.Remove(customer);
            await _uow.CommitAsync();
            return new ServiceResult<CustomerResponse>("", ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<CustomerResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    private async Task<(bool Success, string ErrorMessage)> MapRelatedEntitiesFromIds(
        Customer customer,
        CustomerRequest customerRequest
    )
    {
        var user = await _uow.LocalIdentityUserRepository.FindByIdAsync(customerRequest.UserId);
        if (user == null)
            return (false, "A customer must belong to a valid user (check userId validity).");

        customer.User = user;

        return (true, string.Empty);
    }
}

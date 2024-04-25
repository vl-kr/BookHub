using AutoMapper;
using BusinessLayer.DTOs.Requests.PaymentMethod;
using BusinessLayer.DTOs.Responses.PaymentMethod;
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

public class PaymentMethodService : IPaymentMethodService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public PaymentMethodService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PaymentMethodResponse>> CreatePaymentMethod(
        PaymentMethodRequest paymentMethodRequest
    )
    {
        var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodRequest);
        await _uow.PaymentMethodRepository.AddAsync(paymentMethod);
        await _uow.CommitAsync();
        return new ServiceResult<PaymentMethodResponse>(
            _mapper.Map<PaymentMethodResponse>(paymentMethod),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<PaymentMethodResponse>>> GetPaymentMethods(
        PageOptions pageOptions
    )
    {
        var query = new EFCoreQueryObject<PaymentMethod>(_context);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var paymentMethods = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<PaymentMethodResponse>>(
            paymentMethods.Select(_mapper.Map<PaymentMethodResponse>)
        );
    }

    public async Task<ServiceResult<PaymentMethodResponse>> GetPaymentMethod(int id)
    {
        var paymentMethod = await _uow.PaymentMethodRepository.FindByIdAsync(id);
        if (paymentMethod == null)
            return new ServiceResult<PaymentMethodResponse>(
                "Payment method not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<PaymentMethodResponse>(
            _mapper.Map<PaymentMethodResponse>(paymentMethod)
        );
    }

    public async Task<ServiceResult<PaymentMethodResponse>> UpdatePaymentMethod(
        int id,
        PaymentMethodRequest paymentMethodRequest
    )
    {
        var existingPaymentMethod = await _uow.PaymentMethodRepository.FindByIdAsync(id);
        if (existingPaymentMethod == null)
            return new ServiceResult<PaymentMethodResponse>(
                "Payment method not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.PaymentMethodRepository.Update(
                _mapper.Map(paymentMethodRequest, existingPaymentMethod)
            );
            await _uow.CommitAsync();
            return new ServiceResult<PaymentMethodResponse>(
                _mapper.Map<PaymentMethodResponse>(existingPaymentMethod)
            );
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<PaymentMethodResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<PaymentMethodResponse>> DeletePaymentMethod(int id)
    {
        var paymentMethod = await _uow.PaymentMethodRepository.FindByIdAsync(id);
        if (paymentMethod == null)
            return new ServiceResult<PaymentMethodResponse>(
                "Payment method not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.PaymentMethodRepository.Remove(paymentMethod);
            await _uow.CommitAsync();
            return new ServiceResult<PaymentMethodResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response =
                DbUpdateExceptionHandler.GetServiceResultForException<PaymentMethodResponse>(ex);
            if (response != null)
                return response;
            throw;
        }
    }
}

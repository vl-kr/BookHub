using AutoMapper;
using BusinessLayer.DTOs.Requests.Publisher;
using BusinessLayer.DTOs.Responses.Publisher;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.PublisherFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class PublisherService : IPublisherService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public PublisherService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PublisherResponse>> CreatePublisher(
        PublisherRequest publisherRequest
    )
    {
        var publisher = _mapper.Map<Publisher>(publisherRequest);
        await _uow.PublisherRepository.AddAsync(publisher);
        await _uow.CommitAsync();
        return new ServiceResult<PublisherResponse>(
            _mapper.Map<PublisherResponse>(publisher),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<PublisherResponse>>> GetPublishers(
        PageOptions pageOptions,
        PublisherFilter publisherFilter
    )
    {
        var query = new EFCoreQueryObject<Publisher>(_context);

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(publisherFilter);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var publishers = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<PublisherResponse>>(
            publishers.Select(_mapper.Map<PublisherResponse>)
        );
    }

    public async Task<ServiceResult<PublisherResponse>> GetPublisher(int id)
    {
        var publisher = await _uow.PublisherRepository.FindByIdAsync(id);
        if (publisher == null)
            return new ServiceResult<PublisherResponse>(
                "Publisher not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<PublisherResponse>(_mapper.Map<PublisherResponse>(publisher));
    }

    public async Task<ServiceResult<PublisherResponse>> UpdatePublisher(
        int id,
        PublisherRequest publisherRequest
    )
    {
        var existingPublisher = await _uow.PublisherRepository.FindByIdAsync(id);
        if (existingPublisher == null)
            return new ServiceResult<PublisherResponse>(
                "Publisher not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.PublisherRepository.Update(_mapper.Map(publisherRequest, existingPublisher));
            await _uow.CommitAsync();
            return new ServiceResult<PublisherResponse>(
                _mapper.Map<PublisherResponse>(existingPublisher)
            );
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<PublisherResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<PublisherResponse>> DeletePublisher(int id)
    {
        var publisher = await _uow.PublisherRepository.FindByIdAsync(id);
        if (publisher == null)
            return new ServiceResult<PublisherResponse>(
                "Publisher not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.PublisherRepository.Remove(publisher);
            await _uow.CommitAsync();
            return new ServiceResult<PublisherResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<PublisherResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<PaginationObject<Publisher>> GetPaginatedPublishers(PageOptions pageOptions)
    {
        var query = new EFCoreQueryObject<Publisher>(_context);
        query.SearchFor(pageOptions.SearchTerm);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);
        var result = await query.GetPagedResultAsync(
            pageOptions.Page ?? 1,
            pageOptions.PageSize ?? 10
        );
        return result;
    }
}

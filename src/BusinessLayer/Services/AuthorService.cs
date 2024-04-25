using AutoMapper;
using BusinessLayer.DTOs.Requests.Author;
using BusinessLayer.DTOs.Responses.Author;
using BusinessLayer.Enums;
using BusinessLayer.Helpers.DbUpdateExceptionHandler;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.AuthorFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services;

public class AuthorService : IAuthorService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public AuthorService(IUnitOfWork unitOfWork, BookHubDbContext context, IMapper mapper)
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<AuthorResponse>> CreateAuthor(AuthorRequest authorRequest)
    {
        var author = _mapper.Map<Author>(authorRequest);
        await _uow.AuthorRepository.AddAsync(author);
        await _uow.CommitAsync();
        return new ServiceResult<AuthorResponse>(
            _mapper.Map<AuthorResponse>(author),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<AuthorResponse>>> GetAuthors(
        PageOptions pageOptions,
        AuthorFilter authorFilter
    )
    {
        var query = new EFCoreQueryObject<Author>(_context);

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(authorFilter);

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var authors = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<AuthorResponse>>(
            authors.Select(_mapper.Map<AuthorResponse>)
        );
    }

    public async Task<ServiceResult<AuthorResponse>> GetAuthor(int id)
    {
        var author = await _uow.AuthorRepository.FindByIdAsync(id);
        if (author == null)
            return new ServiceResult<AuthorResponse>(
                "Author not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<AuthorResponse>(_mapper.Map<AuthorResponse>(author));
    }

    public async Task<ServiceResult<AuthorResponse>> UpdateAuthor(
        int id,
        AuthorRequest authorRequest
    )
    {
        var existingAuthor = await _uow.AuthorRepository.FindByIdAsync(id);
        if (existingAuthor == null)
            return new ServiceResult<AuthorResponse>(
                "Author not found",
                ServiceResultCode.NotFound
            );

        try
        {
            _uow.AuthorRepository.Update(_mapper.Map(authorRequest, existingAuthor));
            await _uow.CommitAsync();
            return new ServiceResult<AuthorResponse>(_mapper.Map<AuthorResponse>(existingAuthor));
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<AuthorResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<ServiceResult<AuthorResponse>> DeleteAuthor(int id)
    {
        var author = await _uow.AuthorRepository.FindByIdAsync(id);
        if (author == null)
            return new ServiceResult<AuthorResponse>(
                "Author not found",
                ServiceResultCode.NotFound
            );
        try
        {
            _uow.AuthorRepository.Remove(author);
            await _uow.CommitAsync();
            return new ServiceResult<AuthorResponse>(ServiceResultCode.NoContent);
        }
        catch (DbUpdateException ex)
        {
            var response = DbUpdateExceptionHandler.GetServiceResultForException<AuthorResponse>(
                ex
            );
            if (response != null)
                return response;
            throw;
        }
    }

    public async Task<PaginationObject<Author>> GetPaginatedAuthors(PageOptions pageOptions)
    {
        var query = new EFCoreQueryObject<Author>(_context);
        query.SearchFor(pageOptions.SearchTerm);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);
        var result = await query.GetPagedResultAsync(
            pageOptions.Page ?? 1,
            pageOptions.PageSize ?? 10
        );
        return result;
    }
}

using AutoMapper;
using BusinessLayer.DTOs.Requests.LocalIdentityUser;
using BusinessLayer.DTOs.Responses.LocalIdentityUser;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Query;
using BusinessLayer.Services.Filtering.UserFilters;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.UnitOfWork;

namespace BusinessLayer.Services;

public class LocalIdentityUserService : ILocalIdentityUserService
{
    private readonly BookHubDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public LocalIdentityUserService(
        IUnitOfWork unitOfWork,
        BookHubDbContext context,
        IMapper mapper
    )
    {
        _uow = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<LocalIdentityUserResponse>> CreateLocalIdentityUser(
        LocalIdentityUserRequest localIdentityUserRequest
    )
    {
        var localIdentityUser = _mapper.Map<LocalIdentityUser>(localIdentityUserRequest);
        await _uow.LocalIdentityUserRepository.AddAsync(localIdentityUser);
        await _uow.CommitAsync();
        return new ServiceResult<LocalIdentityUserResponse>(
            _mapper.Map<LocalIdentityUserResponse>(localIdentityUser),
            ServiceResultCode.Created
        );
    }

    public async Task<ServiceResult<IEnumerable<LocalIdentityUserResponse>>> GetLocalIdentityUsers(
        PageOptions pageOptions,
        UserFilter userFilter
    )
    {
        var query = new EFCoreQueryObject<LocalIdentityUser>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder);
        query.Page(pageOptions.Page, pageOptions.PageSize);

        var localIdentityUsers = await query.ExecuteAsync();
        return new ServiceResult<IEnumerable<LocalIdentityUserResponse>>(
            localIdentityUsers.Select(_mapper.Map<LocalIdentityUserResponse>)
        );
    }

    public async Task<ServiceResult<LocalIdentityUserResponse>> GetLocalIdentityUser(int id)
    {
        var localIdentityUser =
            await _uow.LocalIdentityUserRepository.FindByIdWithAllRelatedDataAsync(id);
        if (localIdentityUser == null)
            return new ServiceResult<LocalIdentityUserResponse>(
                "LocalIdentityUser not found",
                ServiceResultCode.NotFound
            );
        return new ServiceResult<LocalIdentityUserResponse>(
            _mapper.Map<LocalIdentityUserResponse>(localIdentityUser)
        );
    }

    public async Task<ServiceResult<LocalIdentityUserResponse>> UpdateLocalIdentityUser(
        int id,
        LocalIdentityUserRequest localIdentityUserRequest
    )
    {
        var existingLocalIdentityUser =
            await _uow.LocalIdentityUserRepository.FindByIdWithAllRelatedDataAsync(id);
        if (existingLocalIdentityUser == null)
            return new ServiceResult<LocalIdentityUserResponse>(
                "LocalIdentityUser not found",
                ServiceResultCode.NotFound
            );

        _uow.LocalIdentityUserRepository.Update(
            _mapper.Map(localIdentityUserRequest, existingLocalIdentityUser)
        );

        await _uow.CommitAsync();

        return new ServiceResult<LocalIdentityUserResponse>(
            _mapper.Map<LocalIdentityUserResponse>(existingLocalIdentityUser)
        );
    }

    public async Task<ServiceResult<LocalIdentityUserResponse>> DeleteLocalIdentityUser(int id)
    {
        var localIdentityUser =
            await _uow.LocalIdentityUserRepository.FindByIdWithAllRelatedDataAsync(id);
        if (localIdentityUser == null)
            return new ServiceResult<LocalIdentityUserResponse>(
                "LocalIdentityUser not found",
                ServiceResultCode.NotFound
            );
        _uow.LocalIdentityUserRepository.Remove(localIdentityUser);
        await _uow.CommitAsync();
        return new ServiceResult<LocalIdentityUserResponse>("", ServiceResultCode.NoContent);
    }

    public async Task<PaginationObject<LocalIdentityUserResponse>> GetPaginatedLocalIdentityUser(
        PageOptions pageOptions,
        UserFilter userFilter
    )
    {
        var query = new EFCoreQueryObject<LocalIdentityUser>(_context);
        query.Include(q => q.IncludeAllRelatedData());

        query.SearchFor(pageOptions.SearchTerm);
        query.FilterOn(userFilter);
        query.OrderBy(pageOptions.SortColumn, pageOptions.SortOrder, true);

        var result = await query.GetPagedResultAsync(
            pageOptions.Page ?? 1,
            pageOptions.PageSize ?? 10
        );
        var responseResult = new PaginationObject<LocalIdentityUserResponse>
        {
            Items = result.Items.Select(_mapper.Map<LocalIdentityUserResponse>),
            TotalItems = result.TotalItems,
            Page = result.Page,
            TotalPages = result.TotalPages
        };
        return responseResult;
    }
}

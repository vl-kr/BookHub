using BusinessLayer.DTOs.Requests.LocalIdentityUser;
using BusinessLayer.DTOs.Responses.LocalIdentityUser;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.UserFilters;
using BusinessLayer.Services.Result;

namespace BusinessLayer.Services.Interfaces;

public interface ILocalIdentityUserService
{
    Task<ServiceResult<LocalIdentityUserResponse>> CreateLocalIdentityUser(
        LocalIdentityUserRequest localIdentityUserRequest
    );

    Task<ServiceResult<IEnumerable<LocalIdentityUserResponse>>> GetLocalIdentityUsers(
        PageOptions pageOptions,
        UserFilter userFilter
    );

    Task<ServiceResult<LocalIdentityUserResponse>> GetLocalIdentityUser(int id);

    Task<ServiceResult<LocalIdentityUserResponse>> UpdateLocalIdentityUser(
        int id,
        LocalIdentityUserRequest localIdentityUserRequest
    );

    Task<ServiceResult<LocalIdentityUserResponse>> DeleteLocalIdentityUser(int id);

    Task<PaginationObject<LocalIdentityUserResponse>> GetPaginatedLocalIdentityUser(
        PageOptions pageOptions,
        UserFilter userFilter
    );
}

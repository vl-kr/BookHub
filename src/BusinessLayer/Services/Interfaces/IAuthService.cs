using System.Security.Claims;
using BusinessLayer.DTOs.Requests.Auth;
using BusinessLayer.DTOs.Responses.Auth;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest loginRequest);

    Task<ServiceResult<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest);

    Task<LocalIdentityUser?> GetUserAsync(ClaimsPrincipal user);

    Task SignOutAsync();
}

using BusinessLayer.DTOs.Requests.Auth;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebAPI.Controllers;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var serviceResult = await _authService.LoginAsync(loginRequest);
        return BuildResponse(serviceResult);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var serviceResult = await _authService.RegisterAsync(registerRequest);
        return BuildResponse(serviceResult);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.SignOutAsync();
        return Empty;
    }
}

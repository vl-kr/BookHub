using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.DTOs.Requests.Auth;
using BusinessLayer.DTOs.Responses.Auth;
using BusinessLayer.Enums;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Services.Result;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly SignInManager<LocalIdentityUser> _signInManager;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public AuthService(
        IConfiguration config,
        UserManager<LocalIdentityUser> userManager,
        SignInManager<LocalIdentityUser> signInManager
    )
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user == null)
            return new ServiceResult<LoginResponse>("User not found", ServiceResultCode.NotFound);

        var result = await _signInManager.PasswordSignInAsync(
            user,
            loginRequest.Password,
            loginRequest.RememberMe,
            false
        );
        if (!result.Succeeded)
            return new ServiceResult<LoginResponse>(
                "Invalid password",
                ServiceResultCode.BadRequest
            );

        return new ServiceResult<LoginResponse>(
            new LoginResponse { Key = CreateToken(loginRequest.Email, user.Id) }
        );
    }

    public async Task<ServiceResult<RegisterResponse>> RegisterAsync(
        RegisterRequest registerRequest
    )
    {
        var user = new LocalIdentityUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            Customer = new Customer
            {
                Email = registerRequest.Email,
                ShoppingCart = new ShoppingCart(),
                Wishlist = new Wishlist()
            }
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
            return new ServiceResult<RegisterResponse>(
                result.Errors.FirstOrDefault()?.Description,
                ServiceResultCode.BadRequest
            );

        return new ServiceResult<RegisterResponse>(
            new RegisterResponse { Key = CreateToken(registerRequest.Email, user.Id) },
            ServiceResultCode.Created
        );
    }

    public async Task<LocalIdentityUser?> GetUserAsync(ClaimsPrincipal user)
    {
        return await _userManager.GetUserAsync(user);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private string CreateToken(string email, int id)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, email),
            new(ClaimTypes.NameIdentifier, id.ToString())
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256Signature
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

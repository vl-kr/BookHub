using AutoMapper;
using BusinessLayer.DTOs.Requests.Auth;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models.Account;

namespace WebMVC.Controllers;

public class AccountController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AccountController(IAuthService authService, IMapper mapper, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userResult = await _authService.RegisterAsync(_mapper.Map<RegisterRequest>(model));
        var handleResult = HandleCreateResult(userResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(nameof(Login), nameof(AccountController).Replace("Controller", ""));
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userResult = await _authService.LoginAsync(_mapper.Map<LoginRequest>(model));
        var handleResult = HandleReadResult(userResult);
        if (handleResult != null)
            return handleResult;

        return RedirectToAction(
            nameof(LoginSuccess),
            nameof(AccountController).Replace("Controller", "")
        );
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.SignOutAsync();
        return RedirectToAction(
            nameof(HomeController.Index),
            nameof(HomeController).Replace("Controller", "")
        );
    }

    public IActionResult LoginSuccess()
    {
        return View();
    }

    public async Task<IActionResult> Profile()
    {
        var user = await _authService.GetUserAsync(User);
        if (user == null)
            return RedirectToAction(nameof(Login));

        return View(_mapper.Map<ProfileViewModel>(user));
    }
}

using System.Net;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Filtering.UserFilters;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Areas.Admin.Models.Account;

namespace WebMVC.Areas.Admin.Controllers;

public class AccountController : AdminController
{
    private readonly ILocalIdentityUserService _localIdentityUserService;
    private readonly IMapper _mapper;
    private readonly SignInManager<LocalIdentityUser> _signInManager;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public AccountController(
        UserManager<LocalIdentityUser> userManager,
        SignInManager<LocalIdentityUser> signInManager,
        ILocalIdentityUserService localIdentityUserService,
        IMapper mapper,
        IMemoryCache memoryCache
    )
        : base(memoryCache)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userManager = userManager;
        _localIdentityUserService = localIdentityUserService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(
        [FromQuery] PageOptions options,
        [FromQuery] UserFilter userFilter
    )
    {
        var users = await _localIdentityUserService.GetPaginatedLocalIdentityUser(
            options,
            userFilter
        );
        return View(users);
    }

    public async Task<IActionResult> ResetPassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return HandleError("User not found", HttpStatusCode.InternalServerError);

        return View(_mapper.Map<ResetPasswordModel>(user));
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string id, ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return HandleError("User not found", HttpStatusCode.InternalServerError);

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);
        if (result.Succeeded)
            return RedirectToAction(
                nameof(Index),
                nameof(AccountController).Replace("Controller", "")
            );

        return HandleError(
            string.Join('\n', result.Errors.Select(x => x.Description)),
            HttpStatusCode.BadRequest
        );
    }
}

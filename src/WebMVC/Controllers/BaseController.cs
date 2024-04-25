using System.Diagnostics;
using System.Net;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using WebMVC.Models.Shared;

namespace WebMVC.Controllers;

public abstract class BaseController : Controller
{
    protected readonly IMemoryCache MemoryCache;

    protected BaseController(IMemoryCache memoryCache)
    {
        MemoryCache = memoryCache;
    }

    protected IActionResult HandleError(string? errorMessage, HttpStatusCode serviceResultCode)
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ErrorMessage = errorMessage,
            StatusCode = serviceResultCode
        };

        return View("Error", errorViewModel);
    }

    protected IActionResult HandleServiceResultError(
        string? errorMessage,
        ServiceResultCode serviceResultCode
    )
    {
        return HandleError(errorMessage, (HttpStatusCode)serviceResultCode);
    }

    protected IActionResult? HandleResult<T>(ServiceResult<T> result, ServiceResultCode expected)
    {
        if (result.StatusCode == expected)
            return null;

        return HandleServiceResultError(result.ErrorMessage, result.StatusCode);
    }

    protected IActionResult? HandleCreateResult<T>(ServiceResult<T> createResult)
    {
        return HandleResult(createResult, ServiceResultCode.Created);
    }

    protected IActionResult? HandleReadResult<T>(ServiceResult<T> readResult)
    {
        return HandleResult(readResult, ServiceResultCode.OK);
    }

    protected IActionResult? HandleEditResult<T>(ServiceResult<T> editResult)
    {
        return HandleResult(editResult, ServiceResultCode.OK);
    }

    protected IActionResult? HandleDeleteResult<T>(ServiceResult<T> deleteResult)
    {
        return HandleResult(deleteResult, ServiceResultCode.NoContent);
    }

    protected void SaveSource(object item, string source, string cacheKey)
    {
        var cts = MemoryCache.Get<CancellationTokenSource>(source);
        if (cts == null)
            return;
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
            .AddExpirationToken(new CancellationChangeToken(cts.Token));

        MemoryCache.Set(cacheKey, item, cacheEntryOptions);
    }

    protected void SetTokenSource(string tokenSource)
    {
        var cts = MemoryCache.Get<CancellationTokenSource>(tokenSource);
        if (cts != null)
            return;

        MemoryCache.Set(tokenSource, new CancellationTokenSource());
    }

    protected void InvalidateTokenSource(string tokenSource)
    {
        var cts = MemoryCache.Get<CancellationTokenSource>(tokenSource);
        if (cts == null)
            return;

        cts.Cancel();
        cts.Dispose();
        MemoryCache.Set(tokenSource, new CancellationTokenSource());
    }

    protected string CreateCacheKeyFromPageOptions(PageOptions pageOptions, string identifier)
    {
        return $"{identifier}_{pageOptions.Page ?? 1}_{pageOptions.PageSize ?? 10}_{pageOptions.SortColumn}_{pageOptions.SortOrder}";
    }
}

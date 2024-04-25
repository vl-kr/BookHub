using System.Net;
using BusinessLayer.Enums;
using BusinessLayer.Models;
using BusinessLayer.Services.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly IMemoryCache MemoryCache;

    protected BaseController(IMemoryCache memoryCache)
    {
        MemoryCache = memoryCache;
    }

    protected IActionResult BuildResponse<T>(ServiceResult<T>? serviceResult)
    {
        if (serviceResult == null)
            return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to build response.");

        return
            serviceResult.StatusCode
                is ServiceResultCode.OK
                    or ServiceResultCode.Created
                    or ServiceResultCode.NoContent
            ? StatusCode((int)serviceResult.StatusCode, serviceResult.Data)
            : StatusCode((int)serviceResult.StatusCode, serviceResult.ErrorMessage);
    }

    protected IActionResult MaybeInvalidateSourceAndBuildResponse<T>(
        ServiceResult<T> serviceResult,
        string source
    )
    {
        if (
            serviceResult.StatusCode
            is ServiceResultCode.OK
                or ServiceResultCode.Created
                or ServiceResultCode.NoContent
        )
        {
            InvalidateTokenSource(source);
            return StatusCode((int)serviceResult.StatusCode, serviceResult.Data);
        }

        return StatusCode((int)serviceResult.StatusCode, serviceResult.ErrorMessage);
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

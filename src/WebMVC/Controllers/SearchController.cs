using BusinessLayer.Coordinators.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models.Shared;

namespace WebMVC.Controllers;

[Authorize]
public class SearchController : BaseController
{
    private readonly ISearchCoordinator _searchCoordinator;

    public SearchController(ISearchCoordinator searchCoordinator, IMemoryCache memoryCache)
        : base(memoryCache)
    {
        _searchCoordinator = searchCoordinator;
    }

    public async Task<IActionResult> Search(string searchTerm)
    {
        var results = await _searchCoordinator.Search(searchTerm);
        var searchModel = new GlobalSearchViewModel { SearchTerm = searchTerm, Results = results };
        return View(searchModel);
    }
}

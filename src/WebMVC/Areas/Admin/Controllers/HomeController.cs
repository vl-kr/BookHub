using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebMVC.Areas.Admin.Controllers;

public class HomeController : AdminController
{
    public HomeController(IMemoryCache memoryCache)
        : base(memoryCache) { }

    public IActionResult Index()
    {
        return View();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebMVC.Controllers;

public class HomeController : BaseController
{
    public HomeController(IMemoryCache memoryCache)
        : base(memoryCache) { }

    public IActionResult Index()
    {
        return View();
    }
}

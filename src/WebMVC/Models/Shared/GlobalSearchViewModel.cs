using BusinessLayer.Models;

namespace WebMVC.Models.Shared;

public class GlobalSearchViewModel
{
    public string? SearchTerm { get; set; }
    public SearchResult? Results { get; set; }
}

using BusinessLayer.Services.Filtering;

namespace WebMVC.Models.Shared;

public class SearchAndFilterViewModel
{
    public string? ControllerName { get; set; }
    public string? FilterLocation { get; set; }
    public List<string> SortOptions { get; set; } = new();
    public EntityFilter? Filter { get; set; }
    public string? Title { get; set; }
    public bool IncludeCreate { get; set; } = false;
}

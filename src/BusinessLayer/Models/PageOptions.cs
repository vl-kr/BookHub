namespace BusinessLayer.Models;

public class PageOptions
{
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}

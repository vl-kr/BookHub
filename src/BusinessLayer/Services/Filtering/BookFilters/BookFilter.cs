namespace BusinessLayer.Services.Filtering.BookFilters;

public class BookFilter : EntityFilter
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? PriceFrom { get; set; }

    public int? PriceTo { get; set; }

    public int? YearPublishedFrom { get; set; }

    public int? YearPublishedTo { get; set; }

    public string? Authors { get; set; }

    public string? Publishers { get; set; }

    public string? Genres { get; set; }
}

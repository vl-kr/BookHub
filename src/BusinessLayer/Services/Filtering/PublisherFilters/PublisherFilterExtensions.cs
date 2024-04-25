using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.PublisherFilters;

public static class PublisherFilterExtensions
{
    public static void SearchFor(this EFCoreQueryObject<Publisher> query, string? searchTerm)
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b => b.Name != null && b.Name.ToLower().Contains(normalizedSearchTerm));
    }

    public static void FilterOn(
        this EFCoreQueryObject<Publisher> query,
        PublisherFilter publisherFilter
    )
    {
        if (!string.IsNullOrWhiteSpace(publisherFilter.Name))
            query.Filter(b => b.Name != null && b.Name.Contains(publisherFilter.Name));
    }
}

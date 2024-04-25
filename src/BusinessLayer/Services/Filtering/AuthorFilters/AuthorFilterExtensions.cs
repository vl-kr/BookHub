using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.AuthorFilters;

public static class AuthorFilterMethods
{
    public static void SearchFor(this EFCoreQueryObject<Author> query, string? searchTerm)
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b => b.Name != null && b.Name.ToLower().Contains(normalizedSearchTerm));
    }

    public static void FilterOn(this EFCoreQueryObject<Author> query, AuthorFilter authorFilter)
    {
        if (!string.IsNullOrWhiteSpace(authorFilter.Name))
            query.Filter(b => b.Name != null && b.Name.Contains(authorFilter.Name));
    }
}

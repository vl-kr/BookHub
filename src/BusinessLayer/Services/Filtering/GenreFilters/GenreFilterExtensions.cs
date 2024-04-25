using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.GenreFilters;

public static class GenreFilterExtensions
{
    public static void SearchFor(this EFCoreQueryObject<Genre> query, string? searchTerm)
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b => b.Name != null && b.Name.ToLower().Contains(normalizedSearchTerm));
    }

    public static void FilterOn(this EFCoreQueryObject<Genre> query, GenreFilter genreFilter)
    {
        if (!string.IsNullOrWhiteSpace(genreFilter.Name))
            query.Filter(b => b.Name != null && b.Name.Contains(genreFilter.Name));
    }
}

using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.BookFilters;

public static class BookFilterExtensions
{
    public static void SearchFor(this EFCoreQueryObject<Book> query, string? searchTerm)
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b =>
                (b.Title != null && b.Title.ToLower().Contains(normalizedSearchTerm))
                || (b.Description != null && b.Description.ToLower().Contains(normalizedSearchTerm))
                || b.Authors.Any(a =>
                    a.Name != null && a.Name.ToLower().Contains(normalizedSearchTerm)
                )
            );
    }

    public static void FilterOn(this EFCoreQueryObject<Book> query, BookFilter bookFilter)
    {
        if (!string.IsNullOrWhiteSpace(bookFilter.Title))
            query.Filter(b => b.Title != null && b.Title.Contains(bookFilter.Title));

        if (!string.IsNullOrWhiteSpace(bookFilter.Description))
            query.Filter(b =>
                b.Description != null && b.Description.Contains(bookFilter.Description)
            );

        if (bookFilter.PriceFrom.HasValue)
            query.Filter(b => b.Price >= bookFilter.PriceFrom);

        if (bookFilter.PriceTo.HasValue)
            query.Filter(b => b.Price <= bookFilter.PriceTo);

        if (bookFilter.YearPublishedFrom.HasValue)
            query.Filter(b => b.YearPublished >= bookFilter.YearPublishedFrom);

        if (bookFilter.YearPublishedTo.HasValue)
            query.Filter(b => b.YearPublished <= bookFilter.YearPublishedTo);

        if (!string.IsNullOrWhiteSpace(bookFilter.Authors))
        {
            var selectedAuthors = bookFilter.Authors.Split(',').Select(x => x.Trim());
            query.Filter(b => b.Authors.Any(g => selectedAuthors.Contains(g.Name)));
        }

        if (!string.IsNullOrWhiteSpace(bookFilter.Publishers))
        {
            var selectedPublishers = bookFilter.Publishers.Split(',').Select(x => x.Trim());
            query.Filter(b => b.Publisher != null && selectedPublishers.Contains(b.Publisher.Name));
        }

        if (!string.IsNullOrWhiteSpace(bookFilter.Genres))
        {
            var selectedGenres = bookFilter.Genres.Split(',').Select(x => x.Trim());
            query.Filter(b => b.Genres.Any(g => selectedGenres.Contains(g.Name)));
        }
    }
}

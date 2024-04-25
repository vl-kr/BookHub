using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.ReviewFilters;

public static class ReviewFilterExtensions
{
    public static void SearchFor(this EFCoreQueryObject<Review> query, string? searchTerm)
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b =>
                b.TextReview != null && b.TextReview.ToLower().Contains(normalizedSearchTerm)
            );
    }

    public static void FilterOn(this EFCoreQueryObject<Review> query, ReviewFilter reviewFilter)
    {
        if (reviewFilter.Rating != null)
            query.Filter(b => b.Rating.Equals(reviewFilter.Rating));
    }
}

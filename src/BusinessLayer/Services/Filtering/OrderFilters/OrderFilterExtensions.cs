using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.OrderFilters;

public static class OrderFilterExtensions
{
    public static void SearchFor(this EFCoreQueryObject<Order> query, string? searchTerm)
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b => b.Note != null && b.Note.ToLower().Contains(normalizedSearchTerm));
    }

    public static void FilterOn(this EFCoreQueryObject<Order> query, OrderFilter orderFilter)
    {
        if (orderFilter.IsPaid != null)
            query.Filter(b => b.IsPaid.Equals(orderFilter.IsPaid));

        if (!string.IsNullOrWhiteSpace(orderFilter.OrderStatuses))
        {
            var selectedOrderStatuses = orderFilter.OrderStatuses.Split(',').Select(x => x.Trim());
            query.Filter(b => b.Status != null && selectedOrderStatuses.Contains(b.Status.Name));
        }
    }
}

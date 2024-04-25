using BusinessLayer.Query;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Filtering.UserFilters;

public static class UserFilterExtensions
{
    public static void SearchFor(
        this EFCoreQueryObject<LocalIdentityUser> query,
        string? searchTerm
    )
    {
        var normalizedSearchTerm = searchTerm?.ToLower();
        if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
            query.Filter(b =>
                b.UserName != null && b.UserName.ToLower().Contains(normalizedSearchTerm)
            );
    }

    public static void FilterOn(
        this EFCoreQueryObject<LocalIdentityUser> query,
        UserFilter userFilter
    )
    {
        if (!string.IsNullOrWhiteSpace(userFilter.Email))
            query.Filter(b => b.Email != null && b.Email.Contains(userFilter.Email));

        if (!string.IsNullOrWhiteSpace(userFilter.UserName))
            query.Filter(b => b.UserName != null && b.UserName.Contains(userFilter.UserName));
    }
}

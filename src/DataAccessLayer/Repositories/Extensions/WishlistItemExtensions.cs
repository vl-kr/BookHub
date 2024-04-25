using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class WishlistItemExtensions
{
    public static IQueryable<WishlistItem> IncludeAllRelatedData(
        this IQueryable<WishlistItem> wishlistItem
    )
    {
        return wishlistItem.Include(b => b.Book);
    }
}

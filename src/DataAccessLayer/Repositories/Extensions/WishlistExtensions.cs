using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class WishlistExtensions
{
    public static IQueryable<Wishlist> IncludeAllRelatedData(this IQueryable<Wishlist> wishlists)
    {
        return wishlists
            .Include(b => b.WishlistItems)
            .ThenInclude(i => i.Book)
            .Include(x => x.Customer);
    }
}

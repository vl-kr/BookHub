using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class ShoppingCartExtensions
{
    public static IQueryable<ShoppingCart> IncludeAllRelatedData(
        this IQueryable<ShoppingCart> shoppingCarts
    )
    {
        return shoppingCarts
            .Include(b => b.ShoppingCartItems)
            .ThenInclude(s => s.Book)
            .Include(x => x.Customer);
    }
}

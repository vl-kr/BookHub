using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class ShoppingCartItemExtensions
{
    public static IQueryable<ShoppingCartItem> IncludeAllRelatedData(
        this IQueryable<ShoppingCartItem> shoppingCartItem
    )
    {
        return shoppingCartItem.Include(b => b.Book);
    }
}

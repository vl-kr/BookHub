using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class CustomerExtensions
{
    public static IQueryable<Customer> IncludeAllRelatedData(this IQueryable<Customer> customers)
    {
        return customers
            .Include(b => b.Wishlist)
            .Include(b => b.ShoppingCart)
            .Include(b => b.Orders)
            .ThenInclude(x => x.Status)
            .Include(b => b.PreferredBillingAddress)
            .Include(b => b.PreferredShippingAddress);
    }
}

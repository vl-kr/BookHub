using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class OrderExtensions
{
    public static IQueryable<Order> IncludeAllRelatedData(this IQueryable<Order> orders)
    {
        return orders
            .Include(b => b.Status)
            .Include(b => b.Customer)
            .Include(b => b.ShippingAddress)
            .Include(b => b.BillingAddress)
            .Include(b => b.ShippingMethod)
            .Include(b => b.PaymentMethod)
            .Include(b => b.OrderItems)
            .ThenInclude(oi => oi.Book);
    }
}

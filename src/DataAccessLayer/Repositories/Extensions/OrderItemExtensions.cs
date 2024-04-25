using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class OrderItemExtensions
{
    public static IQueryable<OrderItem> IncludeAllRelatedData(this IQueryable<OrderItem> orderItems)
    {
        return orderItems.Include(b => b.Book);
    }
}

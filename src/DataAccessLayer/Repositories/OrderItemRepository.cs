using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<OrderItem>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.OrderItems.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<OrderItem?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context
            .OrderItems.IncludeAllRelatedData()
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}

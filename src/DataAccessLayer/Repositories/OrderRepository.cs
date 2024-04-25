using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Order>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.Orders.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<Order?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context.Orders.IncludeAllRelatedData().FirstOrDefaultAsync(b => b.Id == id);
    }
}

using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<ShoppingCart>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.ShoppingCarts.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<ShoppingCart?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context
            .ShoppingCarts.IncludeAllRelatedData()
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}

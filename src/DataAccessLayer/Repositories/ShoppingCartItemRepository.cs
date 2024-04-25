using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ShoppingCartItemRepository
    : GenericRepository<ShoppingCartItem>,
        IShoppingCartItemRepository
{
    public ShoppingCartItemRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<ShoppingCartItem>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.ShoppingCartItems.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<ShoppingCartItem?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context
            .ShoppingCartItems.IncludeAllRelatedData()
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}

using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class WishlistItemRepository : GenericRepository<WishlistItem>, IWishlistItemRepository
{
    public WishlistItemRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<WishlistItem>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.WishlistItems.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<WishlistItem?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context
            .WishlistItems.IncludeAllRelatedData()
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}

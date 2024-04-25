using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class WishlistRepository : GenericRepository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Wishlist>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.Wishlists.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<Wishlist?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context.Wishlists.IncludeAllRelatedData().FirstOrDefaultAsync(b => b.Id == id);
    }
}

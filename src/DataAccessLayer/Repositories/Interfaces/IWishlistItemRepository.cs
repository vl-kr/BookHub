using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IWishlistItemRepository : IGenericRepository<WishlistItem>
{
    Task<IEnumerable<WishlistItem>> GetAllWithAllRelatedDataAsync();
    Task<WishlistItem?> FindByIdWithAllRelatedDataAsync(int id);
}

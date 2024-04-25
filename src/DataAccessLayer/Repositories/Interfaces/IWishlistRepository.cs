using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist>
{
    Task<IEnumerable<Wishlist>> GetAllWithAllRelatedDataAsync();
    Task<Wishlist?> FindByIdWithAllRelatedDataAsync(int id);
}

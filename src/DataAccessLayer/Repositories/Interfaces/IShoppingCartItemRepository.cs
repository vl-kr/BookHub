using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IShoppingCartItemRepository : IGenericRepository<ShoppingCartItem>
{
    Task<IEnumerable<ShoppingCartItem>> GetAllWithAllRelatedDataAsync();
    Task<ShoppingCartItem?> FindByIdWithAllRelatedDataAsync(int id);
}

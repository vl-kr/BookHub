using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
{
    Task<IEnumerable<ShoppingCart>> GetAllWithAllRelatedDataAsync();
    Task<ShoppingCart?> FindByIdWithAllRelatedDataAsync(int id);
}

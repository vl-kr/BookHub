using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetAllWithAllRelatedDataAsync();
    Task<OrderItem?> FindByIdWithAllRelatedDataAsync(int id);
}

using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetAllWithAllRelatedDataAsync();
    Task<Order?> FindByIdWithAllRelatedDataAsync(int id);
}

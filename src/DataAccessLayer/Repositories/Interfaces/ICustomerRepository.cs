using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllWithAllRelatedDataAsync();
    Task<Customer?> FindByIdWithAllRelatedDataAsync(int id);
}

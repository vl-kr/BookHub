using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<IEnumerable<Review>> GetAllWithAllRelatedDataAsync();
    Task<Review?> FindByIdWithAllRelatedDataAsync(int id);
}

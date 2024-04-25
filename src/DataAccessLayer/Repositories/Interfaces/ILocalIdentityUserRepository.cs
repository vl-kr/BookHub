using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface ILocalIdentityUserRepository : IGenericRepository<LocalIdentityUser>
{
    Task<IEnumerable<LocalIdentityUser>> GetAllWithAllRelatedDataAsync();
    Task<LocalIdentityUser?> FindByIdWithAllRelatedDataAsync(int id);
}

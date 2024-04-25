using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> GetAllWithAllRelatedDataAsync();
    Task<Book?> FindByIdWithAllRelatedDataAsync(int id);
    void DiscardChanges(Book book);
}

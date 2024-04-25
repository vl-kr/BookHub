using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Book>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.Books.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<Book?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context.Books.IncludeAllRelatedData().FirstOrDefaultAsync(b => b.Id == id);
    }

    public void DiscardChanges(Book book)
    {
        EntityEntry entry = Context.Entry(book);
        entry.State = EntityState.Unchanged;
    }
}

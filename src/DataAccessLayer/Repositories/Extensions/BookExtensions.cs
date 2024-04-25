using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class BookExtensions
{
    public static IQueryable<Book> IncludeAllRelatedData(this IQueryable<Book> books)
    {
        return books
            .Include(b => b.PrimaryGenre)
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.Reviews)
            .ThenInclude(r => r.Customer)
            .Include(b => b.Genres);
    }
}

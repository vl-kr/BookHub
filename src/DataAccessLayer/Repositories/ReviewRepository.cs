using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Review>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.Reviews.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<Review?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context.Reviews.IncludeAllRelatedData().FirstOrDefaultAsync(b => b.Id == id);
    }
}

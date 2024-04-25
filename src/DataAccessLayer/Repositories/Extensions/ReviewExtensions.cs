using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class ReviewExtensions
{
    public static IQueryable<Review> IncludeAllRelatedData(this IQueryable<Review> reviews)
    {
        return reviews.Include(b => b.Customer).Include(b => b.Book);
    }
}

using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Extensions;

public static class IdentityUserExtensions
{
    public static IQueryable<LocalIdentityUser> IncludeAllRelatedData(
        this IQueryable<LocalIdentityUser> identityUsers
    )
    {
        return identityUsers.Include(b => b.Customer);
    }
}

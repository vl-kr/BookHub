using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class LocalIdentityUserRepository
    : GenericRepository<LocalIdentityUser>,
        ILocalIdentityUserRepository
{
    private readonly UserManager<LocalIdentityUser> _userManager;

    public LocalIdentityUserRepository(
        BookHubDbContext context,
        UserManager<LocalIdentityUser> userManager
    )
        : base(context)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<LocalIdentityUser>> GetAllWithAllRelatedDataAsync()
    {
        return await _userManager.Users.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<LocalIdentityUser?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await _userManager
            .Users.IncludeAllRelatedData()
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}

using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Seeding;

internal static class IdentityUserRoleSeeder
{
    internal static List<IdentityUserRole<int>> PrepareUserRoleModels()
    {
        return new List<IdentityUserRole<int>>
        {
            new() { RoleId = 1, UserId = 1 }
        };
    }
}

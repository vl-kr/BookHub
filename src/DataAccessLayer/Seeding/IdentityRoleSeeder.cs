using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Seeding;

internal static class IdentityRoleSeeder
{
    internal static List<IdentityRole<int>> PrepareRoleModels()
    {
        return new List<IdentityRole<int>>
        {
            new()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        };
    }
}

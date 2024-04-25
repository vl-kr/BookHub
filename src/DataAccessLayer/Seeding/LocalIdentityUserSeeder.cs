using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Seeding;

internal static class LocalIdentityUserSeeder
{
    internal static List<LocalIdentityUser> PrepareUserModels()
    {
        List<LocalIdentityUser> users =
            new()
            {
                new LocalIdentityUser
                {
                    Id = 1,
                    UserName = "test.admin",
                    Email = "test.admin@mail.com",
                    PhoneNumber = "5551346798",
                    CustomerId = 1
                },
                new LocalIdentityUser
                {
                    Id = 2,
                    UserName = "test.user",
                    Email = "test.user@mail.com",
                    PhoneNumber = "5553164978",
                    CustomerId = 2
                },
                new LocalIdentityUser
                {
                    Id = 3,
                    UserName = "john.martin",
                    Email = "john.martin@mail.com",
                    PhoneNumber = "5551234567",
                    CustomerId = 3
                },
                new LocalIdentityUser
                {
                    Id = 4,
                    UserName = "jane.jones",
                    Email = "jane.jones@mail.com",
                    PhoneNumber = "5559876543",
                    CustomerId = 4
                },
                new LocalIdentityUser
                {
                    Id = 5,
                    UserName = "jack.peters",
                    Email = "jack.peters@mail.com",
                    PhoneNumber = "5552109876",
                    CustomerId = 5
                },
                new LocalIdentityUser
                {
                    Id = 6,
                    UserName = "jill.kowalski",
                    Email = "jill.kowalski@mail.com",
                    PhoneNumber = "5556789054",
                    CustomerId = 6
                },
                new LocalIdentityUser
                {
                    Id = 7,
                    UserName = "james.smith",
                    Email = "james.smith@mail.com",
                    PhoneNumber = "5551234509",
                    CustomerId = 7
                },
                new LocalIdentityUser
                {
                    Id = 8,
                    UserName = "jennifer.roberts",
                    Email = "jennifer.roberts@mail.com",
                    PhoneNumber = "5550987654",
                    CustomerId = 8
                },
                new LocalIdentityUser
                {
                    Id = 9,
                    UserName = "jeffrey.taylor",
                    Email = "jeffrey.taylor@mail.com",
                    PhoneNumber = "5555432167",
                    CustomerId = 9
                },
                new LocalIdentityUser
                {
                    Id = 10,
                    UserName = "jessica.wilson",
                    Email = "jessica.wilson@mail.com",
                    PhoneNumber = "5558765432",
                    CustomerId = 10
                },
                new LocalIdentityUser
                {
                    Id = 11,
                    UserName = "jeremy.anderson",
                    Email = "jeremy.anderson@mail.com",
                    PhoneNumber = "5554321098",
                    CustomerId = 11
                },
                new LocalIdentityUser
                {
                    Id = 12,
                    UserName = "julia.brown",
                    Email = "julia.brown@mail.com",
                    PhoneNumber = "5553210987",
                    CustomerId = 12
                }
            };

        var passwordHasher = new PasswordHasher<LocalIdentityUser>();
        var password = "password";
        var normalizer = new UpperInvariantLookupNormalizer();

        foreach (var user in users)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, password);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.NormalizedUserName = normalizer.NormalizeName(user.UserName);
            user.NormalizedEmail = normalizer.NormalizeName(user.Email);
        }

        return users;
    }
}

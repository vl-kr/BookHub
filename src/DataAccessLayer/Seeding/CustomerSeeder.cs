using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class CustomerSeeder
{
    internal static List<Customer> PrepareCustomerModels()
    {
        return new List<Customer>
        {
            new()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Admin",
                Email = "test.admin@mail.com",
                PhoneNumber = "5551346798",
                UserId = 1
            },
            new()
            {
                Id = 2,
                FirstName = "Test",
                LastName = "User",
                Email = "test.user@mail.com",
                PhoneNumber = "5553164978",
                UserId = 2
            },
            new()
            {
                Id = 3,
                FirstName = "John",
                LastName = "Martin",
                Email = "john.martin@mail.com",
                PhoneNumber = "5551234567",
                UserId = 3
            },
            new()
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Jones",
                Email = "jane.jones@mail.com",
                PhoneNumber = "5559876543",
                UserId = 4
            },
            new()
            {
                Id = 5,
                FirstName = "Jack",
                LastName = "Peters",
                Email = "jack.peters@mail.com",
                PhoneNumber = "5552109876",
                UserId = 5
            },
            new()
            {
                Id = 6,
                FirstName = "Jill",
                LastName = "Kowalski",
                Email = "jill.kowalski@mail.com",
                PhoneNumber = "5556789054",
                UserId = 6
            },
            new()
            {
                Id = 7,
                FirstName = "James",
                LastName = "Smith",
                Email = "james.smith@mail.com",
                PhoneNumber = "5551234509",
                UserId = 7
            },
            new()
            {
                Id = 8,
                FirstName = "Jennifer",
                LastName = "Roberts",
                Email = "jennifer.roberts@mail.com",
                PhoneNumber = "5550987654",
                UserId = 8
            },
            new()
            {
                Id = 9,
                FirstName = "Jeffrey",
                LastName = "Taylor",
                Email = "jeffrey.taylor@mail.com",
                PhoneNumber = "5555432167",
                UserId = 9
            },
            new()
            {
                Id = 10,
                FirstName = "Jessica",
                LastName = "Wilson",
                Email = "jessica.wilson@mail.com",
                PhoneNumber = "5558765432",
                UserId = 10
            },
            new()
            {
                Id = 11,
                FirstName = "Jeremy",
                LastName = "Anderson",
                Email = "jeremy.anderson@mail.com",
                PhoneNumber = "5554321098",
                UserId = 11
            },
            new()
            {
                Id = 12,
                FirstName = "Julia",
                LastName = "Brown",
                Email = "julia.brown@mail.com",
                PhoneNumber = "5553210987",
                UserId = 12
            }
        };
    }
}

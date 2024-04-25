using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class CustomerSeeder
{
    public static List<Customer> PrepareCustomerModels()
    {
        return new List<Customer>
        {
            new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Martin",
                Email = "john.martin@mail.com",
                PhoneNumber = "5551234567"
            },
            new Customer
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Jones",
                Email = "jane.jones@mail.com",
                PhoneNumber = "5559876543"
            },
            new Customer
            {
                Id = 3,
                FirstName = "Jack",
                LastName = "Peters",
                Email = "jack.peters@mail.com",
                PhoneNumber = "5552109876"
            },
            new Customer
            {
                Id = 4,
                FirstName = "Jill",
                LastName = "Kowalski",
                Email = "jill.kowalski@mail.com",
                PhoneNumber = "5556789054"
            },
            new Customer
            {
                Id = 5,
                FirstName = "James",
                LastName = "Smith",
                Email = "james.smith@mail.com",
                PhoneNumber = "5551234509"
            },
            new Customer
            {
                Id = 6,
                FirstName = "Jennifer",
                LastName = "Roberts",
                Email = "jennifer.roberts@mail.com",
                PhoneNumber = "5550987654"
            },
            new Customer
            {
                Id = 7,
                FirstName = "Jeffrey",
                LastName = "Taylor",
                Email = "jeffrey.taylor@mail.com",
                PhoneNumber = "5555432167"
            },
            new Customer
            {
                Id = 8,
                FirstName = "Jessica",
                LastName = "Wilson",
                Email = "jessica.wilson@mail.com",
                PhoneNumber = "5558765432"
            },
            new Customer
            {
                Id = 9,
                FirstName = "Jeremy",
                LastName = "Anderson",
                Email = "jeremy.anderson@mail.com",
                PhoneNumber = "5554321098"
            },
            new Customer
            {
                Id = 10,
                FirstName = "Julia",
                LastName = "Brown",
                Email = "julia.brown@mail.com",
                PhoneNumber = "5553210987"
            }
        };
    }
}

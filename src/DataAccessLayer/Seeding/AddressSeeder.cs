using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal class AddressSeeder
{
    internal static List<Address> PrepareAddressModels()
    {
        return new List<Address>
        {
            new()
            {
                Id = 1,
                Street = "123 Main St",
                PostalCode = "12345",
                City = "New York",
                Country = "USA"
            },
            new()
            {
                Id = 2,
                Street = "456 Elm St",
                PostalCode = "67890",
                City = "Los Angeles",
                Country = "USA"
            },
            new()
            {
                Id = 3,
                Street = "789 Oak St",
                PostalCode = "54321",
                City = "Chicago",
                Country = "USA"
            },
            new()
            {
                Id = 4,
                Street = "101 Pine St",
                PostalCode = "98765",
                City = "Houston",
                Country = "USA"
            },
            new()
            {
                Id = 5,
                Street = "202 Maple St",
                PostalCode = "24680",
                City = "Miami",
                Country = "USA"
            }
        };
    }
}

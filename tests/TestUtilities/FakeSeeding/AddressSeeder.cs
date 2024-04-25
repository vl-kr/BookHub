using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public class AddressSeeder
{
    public static List<Address> PrepareAddressModels()
    {
        return new List<Address>
        {
            new Address
            {
                Id = 1,
                Street = "123 Main St",
                PostalCode = "12345",
                City = "New York",
                Country = "USA"
            },
            new Address
            {
                Id = 2,
                Street = "456 Elm St",
                PostalCode = "67890",
                City = "Los Angeles",
                Country = "USA"
            },
            new Address
            {
                Id = 3,
                Street = "789 Oak St",
                PostalCode = "54321",
                City = "Chicago",
                Country = "USA"
            },
            new Address
            {
                Id = 4,
                Street = "101 Pine St",
                PostalCode = "98765",
                City = "Houston",
                Country = "USA"
            },
            new Address
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

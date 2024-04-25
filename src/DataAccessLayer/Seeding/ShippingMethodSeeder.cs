using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class ShippingMethodSeeder
{
    internal static List<ShippingMethod> PrepareShippingMethodModels()
    {
        return new List<ShippingMethod>
        {
            new()
            {
                Id = 1,
                Name = "DHL",
                Description = "DHL Express - Fast and reliable international shipping"
            },
            new()
            {
                Id = 2,
                Name = "UPS Ground",
                Description = "UPS Ground - Economical domestic shipping"
            },
            new()
            {
                Id = 3,
                Name = "FedEx Standard",
                Description = "FedEx Standard - Standard shipping service"
            }
        };
    }
}

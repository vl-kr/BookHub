using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class ShippingMethodSeeder
{
    public static List<ShippingMethod> PrepareShippingMethodModels()
    {
        return new List<ShippingMethod>
        {
            new ShippingMethod
            {
                Id = 1,
                Name = "DHL",
                Description = "DHL Express - Fast and reliable international shipping"
            },
            new ShippingMethod
            {
                Id = 2,
                Name = "UPS Ground",
                Description = "UPS Ground - Economical domestic shipping"
            },
            new ShippingMethod
            {
                Id = 3,
                Name = "FedEx Standard",
                Description = "FedEx Standard - Standard shipping service"
            }
        };
    }
}

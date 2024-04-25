using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class PaymentMethodSeeder
{
    internal static List<PaymentMethod> PreparePaymentMethodModels()
    {
        return new List<PaymentMethod>
        {
            new()
            {
                Id = 1,
                Name = "Credit Card",
                Description = "Securely pay with your credit card."
            },
            new()
            {
                Id = 2,
                Name = "PayPal",
                Description = "Fast and reliable payments through PayPal."
            },
            new()
            {
                Id = 3,
                Name = "Google Pay",
                Description = "Seamless payments with Google Pay's convenience."
            },
            new()
            {
                Id = 4,
                Name = "Stripe",
                Description = "Flexible payment options with Stripe."
            },
            new()
            {
                Id = 5,
                Name = "Apple Pay",
                Description = "Make easy and secure payments using Apple Pay."
            }
        };
    }
}

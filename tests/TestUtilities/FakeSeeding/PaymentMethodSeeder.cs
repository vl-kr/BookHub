using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class PaymentMethodSeeder
{
    public static List<PaymentMethod> PreparePaymentMethodModels()
    {
        return new List<PaymentMethod>
        {
            new PaymentMethod
            {
                Id = 1,
                Name = "Credit Card",
                Description = "Securely pay with your credit card."
            },
            new PaymentMethod
            {
                Id = 2,
                Name = "PayPal",
                Description = "Fast and reliable payments through PayPal."
            },
            new PaymentMethod
            {
                Id = 3,
                Name = "Google Pay",
                Description = "Seamless payments with Google Pay's convenience."
            },
            new PaymentMethod
            {
                Id = 4,
                Name = "Stripe",
                Description = "Flexible payment options with Stripe."
            },
            new PaymentMethod
            {
                Id = 5,
                Name = "Apple Pay",
                Description = "Make easy and secure payments using Apple Pay."
            }
        };
    }
}

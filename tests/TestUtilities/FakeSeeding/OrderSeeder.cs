using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class OrderSeeder
{
    public static List<Order> PrepareOrderModels()
    {
        return new List<Order>
        {
            new Order
            {
                Id = 1,
                OrderStatusId = 1,
                ShippingAddressId = 1,
                BillingAddressId = 1,
                CustomerId = 1
            },
            new Order
            {
                Id = 2,
                OrderStatusId = 2,
                ShippingAddressId = 2,
                BillingAddressId = 2,
                CustomerId = 2
            },
            new Order
            {
                Id = 3,
                OrderStatusId = 3,
                ShippingAddressId = 3,
                BillingAddressId = 3,
                CustomerId = 3
            },
            new Order
            {
                Id = 4,
                OrderStatusId = 1,
                ShippingAddressId = 4,
                BillingAddressId = 4,
                CustomerId = 4
            },
            new Order
            {
                Id = 5,
                OrderStatusId = 2,
                ShippingAddressId = 5,
                BillingAddressId = 5,
                CustomerId = 5
            }
        };
    }
}

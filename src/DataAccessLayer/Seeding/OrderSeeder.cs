using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class OrderSeeder
{
    internal static List<Order> PrepareOrderModels()
    {
        return new List<Order>
        {
            new()
            {
                Id = 1,
                OrderStatusId = 1,
                ShippingAddressId = 1,
                BillingAddressId = 1,
                CustomerId = 1,
                TotalPrice = 24.99M,
                IsPaid = true
            },
            new()
            {
                Id = 2,
                OrderStatusId = 2,
                ShippingAddressId = 2,
                BillingAddressId = 2,
                TotalPrice = 31.97M,
                CustomerId = 2
            },
            new()
            {
                Id = 3,
                OrderStatusId = 3,
                ShippingAddressId = 3,
                BillingAddressId = 3,
                CustomerId = 3,
                TotalPrice = 11.99M,
                IsPaid = true
            },
            new()
            {
                Id = 4,
                OrderStatusId = 1,
                ShippingAddressId = 4,
                BillingAddressId = 4,
                CustomerId = 4,
                TotalPrice = 29.98M,
                IsPaid = true
            },
            new()
            {
                Id = 5,
                OrderStatusId = 2,
                ShippingAddressId = 5,
                BillingAddressId = 5,
                CustomerId = 5,
                TotalPrice = 61.94M
            }
        };
    }
}

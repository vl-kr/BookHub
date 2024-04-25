using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class OrderItemSeeder
{
    internal static List<OrderItem> PrepareOrderItemModels()
    {
        return new List<OrderItem>
        {
            new()
            {
                Id = 1,
                OrderId = 1,
                BookId = 1,
                Quantity = 1,
                TotalPrice = 24.99M
            },
            new()
            {
                Id = 2,
                OrderId = 2,
                BookId = 2,
                Quantity = 1,
                TotalPrice = 9.99M
            },
            new()
            {
                Id = 3,
                OrderId = 2,
                BookId = 3,
                Quantity = 2,
                TotalPrice = 21.99M
            },
            new()
            {
                Id = 4,
                OrderId = 3,
                BookId = 5,
                Quantity = 1,
                TotalPrice = 11.99M
            },
            new()
            {
                Id = 5,
                OrderId = 4,
                BookId = 6,
                Quantity = 2,
                TotalPrice = 29.98M
            },
            new()
            {
                Id = 6,
                OrderId = 5,
                BookId = 7,
                Quantity = 4,
                TotalPrice = 39.96M
            },
            new()
            {
                Id = 7,
                OrderId = 5,
                BookId = 8,
                Quantity = 2,
                TotalPrice = 21.98M
            }
        };
    }
}

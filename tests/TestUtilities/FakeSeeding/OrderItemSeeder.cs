using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class OrderItemSeeder
{
    public static List<OrderItem> PrepareOrderItemModels()
    {
        return new List<OrderItem>
        {
            new OrderItem
            {
                Id = 1,
                BookId = 1,
                Quantity = 1,
                TotalPrice = 10,
                OrderId = 1
            },
            new OrderItem
            {
                Id = 2,
                OrderId = 2,
                BookId = 2,
                Quantity = 1,
                TotalPrice = 10
            },
            new OrderItem
            {
                Id = 3,
                OrderId = 2,
                BookId = 3,
                Quantity = 2,
                TotalPrice = 20
            },
            new OrderItem
            {
                Id = 4,
                OrderId = 3,
                BookId = 5,
                Quantity = 1,
                TotalPrice = 12
            },
            new OrderItem
            {
                Id = 5,
                OrderId = 4,
                BookId = 6,
                Quantity = 2,
                TotalPrice = 30
            },
            new OrderItem
            {
                Id = 6,
                OrderId = 5,
                BookId = 7,
                Quantity = 4,
                TotalPrice = 40
            },
            new OrderItem
            {
                Id = 7,
                OrderId = 5,
                BookId = 8,
                Quantity = 2,
                TotalPrice = 22
            }
        };
    }
}

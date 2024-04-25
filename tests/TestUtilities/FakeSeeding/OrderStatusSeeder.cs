using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public class OrderStatusSeeder
{
    public static List<OrderStatus> PrepareOrderStatusModels()
    {
        return new List<OrderStatus>
        {
            new OrderStatus { Id = 1, Name = "New" },
            new OrderStatus { Id = 2, Name = "Closed" },
            new OrderStatus { Id = 3, Name = "Cancelled" },
            new OrderStatus { Id = 4, Name = "Payment Received" },
            new OrderStatus { Id = 5, Name = "Payment Failed" },
            new OrderStatus { Id = 6, Name = "In Progress" },
            new OrderStatus { Id = 7, Name = "Completed" }
        };
    }
}

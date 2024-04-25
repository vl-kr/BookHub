using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal class OrderStatusSeeder
{
    internal static List<OrderStatus> PrepareOrderStatusModels()
    {
        return new List<OrderStatus>
        {
            new() { Id = 1, Name = "New" },
            new() { Id = 2, Name = "Closed" },
            new() { Id = 3, Name = "Cancelled" },
            new() { Id = 4, Name = "Payment Received" },
            new() { Id = 5, Name = "Payment Failed" },
            new() { Id = 6, Name = "In Progress" },
            new() { Id = 7, Name = "Completed" }
        };
    }
}

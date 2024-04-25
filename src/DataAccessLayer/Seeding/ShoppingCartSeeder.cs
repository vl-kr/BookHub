using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class ShoppingCartSeeder
{
    internal static List<ShoppingCart> PrepareShoppingCartModels()
    {
        return new List<ShoppingCart>
        {
            new()
            {
                Id = 1,
                TotalPrice = 44.97m,
                CustomerId = 1
            },
            new()
            {
                Id = 2,
                TotalPrice = 80.95m,
                CustomerId = 2
            }
        };
    }
}

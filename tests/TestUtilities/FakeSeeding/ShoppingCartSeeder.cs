using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class ShoppingCartSeeder
{
    public static List<ShoppingCart> PrepareShoppingCartModels()
    {
        return new List<ShoppingCart>
        {
            new ShoppingCart
            {
                Id = 1,
                TotalPrice = 44.97m,
                CustomerId = 1
            },
            new ShoppingCart
            {
                Id = 2,
                TotalPrice = 80.95m,
                CustomerId = 2
            }
        };
    }
}

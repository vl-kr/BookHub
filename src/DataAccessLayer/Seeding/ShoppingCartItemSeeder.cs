using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class ShoppingCartItemSeeder
{
    internal static List<ShoppingCartItem> PrepareShoppingCartItemModels()
    {
        return new List<ShoppingCartItem>
        {
            new()
            {
                Id = 1,
                BookId = 1,
                Quantity = 1,
                TotalPrice = 24.99m,
                ShoppingCartId = 1
            },
            new()
            {
                Id = 2,
                BookId = 2,
                Quantity = 1,
                TotalPrice = 9.99m,
                ShoppingCartId = 1
            },
            new()
            {
                Id = 3,
                BookId = 4,
                Quantity = 1,
                TotalPrice = 9.99m,
                ShoppingCartId = 1
            },
            new()
            {
                Id = 4,
                BookId = 1,
                Quantity = 2,
                TotalPrice = 49.98m,
                ShoppingCartId = 2
            },
            new()
            {
                Id = 5,
                BookId = 3,
                Quantity = 1,
                TotalPrice = 10.99m,
                ShoppingCartId = 2
            },
            new()
            {
                Id = 6,
                BookId = 2,
                Quantity = 1,
                TotalPrice = 9.99m,
                ShoppingCartId = 2
            },
            new()
            {
                Id = 7,
                BookId = 4,
                Quantity = 1,
                TotalPrice = 9.99m,
                ShoppingCartId = 2
            }
        };
    }
}

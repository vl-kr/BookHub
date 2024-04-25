using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class WishlistSeeder
{
    internal static List<Wishlist> PrepareWishlistModels()
    {
        return new List<Wishlist>
        {
            new() { Id = 1, CustomerId = 1 },
            new() { Id = 2, CustomerId = 2 },
            new() { Id = 3, CustomerId = 3 }
        };
    }
}

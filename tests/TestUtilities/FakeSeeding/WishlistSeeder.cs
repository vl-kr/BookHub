using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class WishlistSeeder
{
    public static List<Wishlist> PrepareWishlistModels()
    {
        return new List<Wishlist>
        {
            new Wishlist { Id = 1, CustomerId = 1 },
            new Wishlist { Id = 2, CustomerId = 2 },
            new Wishlist { Id = 3, CustomerId = 3 }
        };
    }
}

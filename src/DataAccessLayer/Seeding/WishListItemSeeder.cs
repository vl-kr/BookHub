using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class WishlistItemSeeder
{
    internal static List<WishlistItem> PrepareWishlistItemModels()
    {
        return new List<WishlistItem>
        {
            new()
            {
                Id = 1,
                BookId = 1,
                WishlistId = 1
            },
            new()
            {
                Id = 2,
                BookId = 8,
                WishlistId = 1
            },
            new()
            {
                Id = 3,
                BookId = 3,
                WishlistId = 1
            },
            new()
            {
                Id = 4,
                BookId = 4,
                WishlistId = 2
            },
            new()
            {
                Id = 5,
                BookId = 7,
                WishlistId = 2
            },
            new()
            {
                Id = 6,
                BookId = 10,
                WishlistId = 2
            },
            new()
            {
                Id = 7,
                BookId = 3,
                WishlistId = 3
            },
            new()
            {
                Id = 8,
                BookId = 2,
                WishlistId = 3
            },
            new()
            {
                Id = 9,
                BookId = 10,
                WishlistId = 3
            }
        };
    }
}

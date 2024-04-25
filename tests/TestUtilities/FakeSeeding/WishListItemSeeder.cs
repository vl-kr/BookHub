using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class WishlistItemSeeder
{
    public static List<WishlistItem> PrepareWishlistItemModels()
    {
        return new List<WishlistItem>
        {
            new WishlistItem
            {
                Id = 1,
                BookId = 1,
                WishlistId = 1
            },
            new WishlistItem
            {
                Id = 2,
                BookId = 8,
                WishlistId = 1
            },
            new WishlistItem
            {
                Id = 3,
                BookId = 3,
                WishlistId = 1
            },
            new WishlistItem
            {
                Id = 4,
                BookId = 4,
                WishlistId = 2
            },
            new WishlistItem
            {
                Id = 5,
                BookId = 7,
                WishlistId = 2
            },
            new WishlistItem
            {
                Id = 6,
                BookId = 10,
                WishlistId = 2
            },
            new WishlistItem
            {
                Id = 7,
                BookId = 3,
                WishlistId = 3
            },
            new WishlistItem
            {
                Id = 8,
                BookId = 2,
                WishlistId = 3
            },
            new WishlistItem
            {
                Id = 9,
                BookId = 10,
                WishlistId = 3
            }
        };
    }
}

using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class BookAuthorSeeder
{
    internal static List<BookAuthor> PrepareBookAuthorModels()
    {
        return new List<BookAuthor>
        {
            new()
            {
                Id = 1,
                BookId = 1,
                AuthorId = 1
            },
            new()
            {
                Id = 2,
                BookId = 1,
                AuthorId = 3
            },
            new()
            {
                Id = 3,
                BookId = 1,
                AuthorId = 5
            },
            new()
            {
                Id = 4,
                BookId = 2,
                AuthorId = 2
            },
            new()
            {
                Id = 5,
                BookId = 3,
                AuthorId = 3
            },
            new()
            {
                Id = 6,
                BookId = 3,
                AuthorId = 6
            },
            new()
            {
                Id = 7,
                BookId = 4,
                AuthorId = 1
            },
            new()
            {
                Id = 8,
                BookId = 4,
                AuthorId = 2
            },
            new()
            {
                Id = 9,
                BookId = 5,
                AuthorId = 2
            },
            new()
            {
                Id = 10,
                BookId = 6,
                AuthorId = 4
            },
            new()
            {
                Id = 11,
                BookId = 6,
                AuthorId = 5
            },
            new()
            {
                Id = 12,
                BookId = 7,
                AuthorId = 6
            },
            new()
            {
                Id = 13,
                BookId = 8,
                AuthorId = 7
            },
            new()
            {
                Id = 14,
                BookId = 9,
                AuthorId = 1
            },
            new()
            {
                Id = 15,
                BookId = 9,
                AuthorId = 5
            },
            new()
            {
                Id = 16,
                BookId = 10,
                AuthorId = 5
            },
            new()
            {
                Id = 17,
                BookId = 11,
                AuthorId = 3
            },
            new()
            {
                Id = 18,
                BookId = 12,
                AuthorId = 7
            },
            new()
            {
                Id = 19,
                BookId = 13,
                AuthorId = 4
            },
            new()
            {
                Id = 20,
                BookId = 14,
                AuthorId = 1
            },
            new()
            {
                Id = 21,
                BookId = 15,
                AuthorId = 2
            },
            new()
            {
                Id = 22,
                BookId = 16,
                AuthorId = 6
            },
            new()
            {
                Id = 23,
                BookId = 17,
                AuthorId = 7
            },
            new()
            {
                Id = 24,
                BookId = 18,
                AuthorId = 5
            },
            new()
            {
                Id = 25,
                BookId = 19,
                AuthorId = 3
            },
            new()
            {
                Id = 26,
                BookId = 19,
                AuthorId = 7
            },
            new()
            {
                Id = 27,
                BookId = 20,
                AuthorId = 2
            }
        };
    }
}

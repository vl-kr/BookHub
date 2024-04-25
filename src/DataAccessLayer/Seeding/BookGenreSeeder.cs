using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class BookGenreSeeder
{
    internal static List<BookGenre> PrepareBookGenreModels()
    {
        return new List<BookGenre>
        {
            new()
            {
                Id = 1,
                BookId = 1,
                GenreId = 1
            },
            new()
            {
                Id = 2,
                BookId = 1,
                GenreId = 3
            },
            new()
            {
                Id = 3,
                BookId = 1,
                GenreId = 5
            },
            new()
            {
                Id = 4,
                BookId = 2,
                GenreId = 2
            },
            new()
            {
                Id = 5,
                BookId = 3,
                GenreId = 3
            },
            new()
            {
                Id = 6,
                BookId = 3,
                GenreId = 6
            },
            new()
            {
                Id = 7,
                BookId = 4,
                GenreId = 4
            },
            new()
            {
                Id = 8,
                BookId = 4,
                GenreId = 2
            },
            new()
            {
                Id = 9,
                BookId = 5,
                GenreId = 2
            },
            new()
            {
                Id = 10,
                BookId = 6,
                GenreId = 10
            },
            new()
            {
                Id = 11,
                BookId = 6,
                GenreId = 5
            },
            new()
            {
                Id = 12,
                BookId = 7,
                GenreId = 6
            },
            new()
            {
                Id = 13,
                BookId = 8,
                GenreId = 11
            },
            new()
            {
                Id = 14,
                BookId = 9,
                GenreId = 1
            },
            new()
            {
                Id = 15,
                BookId = 9,
                GenreId = 5
            },
            new()
            {
                Id = 16,
                BookId = 10,
                GenreId = 5
            },
            new()
            {
                Id = 17,
                BookId = 11,
                GenreId = 8
            },
            new()
            {
                Id = 18,
                BookId = 12,
                GenreId = 5
            },
            new()
            {
                Id = 19,
                BookId = 13,
                GenreId = 11
            },
            new()
            {
                Id = 20,
                BookId = 14,
                GenreId = 1
            },
            new()
            {
                Id = 21,
                BookId = 15,
                GenreId = 8
            },
            new()
            {
                Id = 22,
                BookId = 16,
                GenreId = 6
            },
            new()
            {
                Id = 23,
                BookId = 17,
                GenreId = 7
            },
            new()
            {
                Id = 24,
                BookId = 18,
                GenreId = 9
            },
            new()
            {
                Id = 25,
                BookId = 19,
                GenreId = 3
            },
            new()
            {
                Id = 26,
                BookId = 19,
                GenreId = 12
            },
            new()
            {
                Id = 27,
                BookId = 20,
                GenreId = 10
            }
        };
    }
}

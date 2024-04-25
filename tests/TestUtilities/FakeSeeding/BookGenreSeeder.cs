namespace TestUtilities.FakeSeeding;

public static class BookGenreSeeder
{
    public static List<object> PrepareBookGenreModels()
    {
        return new List<object>
        {
            new { BookId = 1, GenreId = 1 },
            new { BookId = 1, GenreId = 3 },
            new { BookId = 1, GenreId = 5 },
            new { BookId = 2, GenreId = 2 },
            new { BookId = 3, GenreId = 3 },
            new { BookId = 3, GenreId = 6 },
            new { BookId = 4, GenreId = 1 },
            new { BookId = 4, GenreId = 2 },
            new { BookId = 5, GenreId = 2 },
            new { BookId = 6, GenreId = 4 },
            new { BookId = 6, GenreId = 5 },
            new { BookId = 7, GenreId = 6 },
            new { BookId = 8, GenreId = 7 },
            new { BookId = 9, GenreId = 1 },
            new { BookId = 9, GenreId = 5 },
            new { BookId = 10, GenreId = 5 },
            new { BookId = 11, GenreId = 3 },
            new { BookId = 12, GenreId = 7 },
            new { BookId = 13, GenreId = 4 },
            new { BookId = 14, GenreId = 1 },
            new { BookId = 15, GenreId = 2 },
            new { BookId = 16, GenreId = 6 },
            new { BookId = 17, GenreId = 7 },
            new { BookId = 18, GenreId = 5 },
            new { BookId = 19, GenreId = 3 },
            new { BookId = 19, GenreId = 7 },
            new { BookId = 20, GenreId = 2 }
        };
    }
}

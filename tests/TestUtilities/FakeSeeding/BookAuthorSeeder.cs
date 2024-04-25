namespace TestUtilities.FakeSeeding;

public static class BookAuthorSeeder
{
    public static List<object> PrepareBookAuthorModels()
    {
        return new List<object>
        {
            new { BookId = 1, AuthorId = 1 },
            new { BookId = 1, AuthorId = 3 },
            new { BookId = 1, AuthorId = 5 },
            new { BookId = 2, AuthorId = 2 },
            new { BookId = 3, AuthorId = 3 },
            new { BookId = 3, AuthorId = 6 },
            new { BookId = 4, AuthorId = 1 },
            new { BookId = 4, AuthorId = 2 },
            new { BookId = 5, AuthorId = 2 },
            new { BookId = 6, AuthorId = 4 },
            new { BookId = 6, AuthorId = 5 },
            new { BookId = 7, AuthorId = 6 },
            new { BookId = 8, AuthorId = 7 },
            new { BookId = 9, AuthorId = 1 },
            new { BookId = 9, AuthorId = 5 },
            new { BookId = 10, AuthorId = 5 },
            new { BookId = 11, AuthorId = 3 },
            new { BookId = 12, AuthorId = 7 },
            new { BookId = 13, AuthorId = 4 },
            new { BookId = 14, AuthorId = 1 },
            new { BookId = 15, AuthorId = 2 },
            new { BookId = 16, AuthorId = 6 },
            new { BookId = 17, AuthorId = 7 },
            new { BookId = 18, AuthorId = 5 },
            new { BookId = 19, AuthorId = 3 },
            new { BookId = 19, AuthorId = 7 },
            new { BookId = 20, AuthorId = 2 }
        };
    }
}

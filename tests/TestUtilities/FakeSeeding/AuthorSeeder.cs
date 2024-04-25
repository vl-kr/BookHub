using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class AuthorSeeder
{
    public static List<Author> PrepareAuthorModels()
    {
        return new List<Author>
        {
            new Author { Id = 1, Name = "Stephen King" },
            new Author { Id = 2, Name = "J.R.R. Tolkien" },
            new Author { Id = 3, Name = "George R.R. Martin" },
            new Author { Id = 4, Name = "Jo Nesbo" },
            new Author { Id = 5, Name = "Agatha Christie" },
            new Author { Id = 6, Name = "J.K. Rowling" },
            new Author { Id = 7, Name = "Harper Lee" },
            new Author { Id = 8, Name = "Dan Brown" },
            new Author { Id = 9, Name = "Jane Austen" },
            new Author { Id = 10, Name = "William Shakespeare" },
            new Author { Id = 11, Name = "Markus Zusak" },
            new Author { Id = 12, Name = "F. Scott Fitzgerald" },
            new Author { Id = 13, Name = "John Green" },
            new Author { Id = 14, Name = "Khaled Hosseini" },
            new Author { Id = 15, Name = "Veronica Roth" },
            new Author { Id = 16, Name = "Paulo Coelho" },
            new Author { Id = 17, Name = "Paula Hawkins" },
            new Author { Id = 18, Name = "Gillian Flynn" },
            new Author { Id = 19, Name = "E.L. James" }
        };
    }
}

using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class AuthorSeeder
{
    internal static List<Author> PrepareAuthorModels()
    {
        return new List<Author>
        {
            new() { Id = 1, Name = "Stephen King" },
            new() { Id = 2, Name = "J.R.R. Tolkien" },
            new() { Id = 3, Name = "George R.R. Martin" },
            new() { Id = 4, Name = "Jo Nesbo" },
            new() { Id = 5, Name = "Agatha Christie" },
            new() { Id = 6, Name = "J.K. Rowling" },
            new() { Id = 7, Name = "Harper Lee" },
            new() { Id = 8, Name = "Dan Brown" },
            new() { Id = 9, Name = "Jane Austen" },
            new() { Id = 10, Name = "William Shakespeare" },
            new() { Id = 11, Name = "Markus Zusak" },
            new() { Id = 12, Name = "F. Scott Fitzgerald" },
            new() { Id = 13, Name = "John Green" },
            new() { Id = 14, Name = "Khaled Hosseini" },
            new() { Id = 15, Name = "Veronica Roth" },
            new() { Id = 16, Name = "Paulo Coelho" },
            new() { Id = 17, Name = "Paula Hawkins" },
            new() { Id = 18, Name = "Gillian Flynn" },
            new() { Id = 19, Name = "E.L. James" }
        };
    }
}

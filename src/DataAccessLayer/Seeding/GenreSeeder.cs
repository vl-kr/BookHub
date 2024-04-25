using DataAccessLayer.Entities;

namespace DataAccessLayer.Seeding;

internal static class GenreSeeder
{
    internal static List<Genre> PrepareGenreModels()
    {
        return new List<Genre>
        {
            new() { Id = 1, Name = "Fantasy" },
            new() { Id = 2, Name = "Horror" },
            new() { Id = 3, Name = "Crime" },
            new() { Id = 4, Name = "Science Fiction" },
            new() { Id = 5, Name = "Romance" },
            new() { Id = 6, Name = "Mystery" },
            new() { Id = 7, Name = "Thriller" },
            new() { Id = 8, Name = "Comedy" },
            new() { Id = 9, Name = "Drama" },
            new() { Id = 10, Name = "Non-Fiction" },
            new() { Id = 11, Name = "Biography" },
            new() { Id = 12, Name = "History" },
            new() { Id = 13, Name = "Adventure" },
            new() { Id = 14, Name = "Other" }
        };
    }
}

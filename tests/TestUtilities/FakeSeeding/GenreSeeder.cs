using DataAccessLayer.Entities;

namespace TestUtilities.FakeSeeding;

public static class GenreSeeder
{
    public static List<Genre> PrepareGenreModels()
    {
        return new List<Genre>
        {
            new Genre { Id = 1, Name = "Fantasy" },
            new Genre { Id = 2, Name = "Horror" },
            new Genre { Id = 3, Name = "Crime" },
            new Genre { Id = 4, Name = "Science Fiction" },
            new Genre { Id = 5, Name = "Romance" },
            new Genre { Id = 6, Name = "Mystery" },
            new Genre { Id = 7, Name = "Thriller" }
        };
    }
}

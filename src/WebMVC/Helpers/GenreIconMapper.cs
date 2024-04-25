namespace WebMVC.Helpers;

public class GenreIconMapper
{
    public static readonly Dictionary<string, string> IconMap =
        new()
        {
            { "Fantasy", "fas fa-dragon" },
            { "Science Fiction", "fas fa-robot" },
            { "Mystery", "fas fa-search" },
            { "Biography", "fas fa-user" },
            { "History", "fas fa-landmark" },
            { "Romance", "fas fa-heart" },
            { "Horror", "fas fa-ghost" },
            { "Adventure", "fas fa-compass" },
            { "Thriller", "fas fa-user-secret" },
            { "Comedy", "fas fa-laugh" },
            { "Drama", "fas fa-theater-masks" },
            { "Non-Fiction", "fas fa-book-open" },
            { "Other", "fas fa-book" }
        };

    public static string GetIcon(string genre)
    {
        return IconMap.TryGetValue(genre, out var icon) ? icon : IconMap["Other"];
    }
}

namespace DataAccessLayer.Entities;

public class Genre : BaseEntity
{
    public string? Name { get; set; }

    public IEnumerable<Book> Books { get; set; } = new List<Book>();
}

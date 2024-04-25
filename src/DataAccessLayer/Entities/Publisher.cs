namespace DataAccessLayer.Entities;

public class Publisher : BaseEntity
{
    public string? Name { get; set; }

    public IEnumerable<Book> Books { get; set; } = new List<Book>();
}

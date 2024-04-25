namespace BusinessLayer.Models;

public class PaginationObject<T>
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
}

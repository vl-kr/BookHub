namespace BusinessLayer.Services.Filtering.AddressFilters;

public class AddressFilter : EntityFilter
{
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}

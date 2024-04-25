namespace BusinessLayer.Services.Filtering.OrderFilters;

public class OrderFilter : EntityFilter
{
    public bool? IsPaid { get; set; }

    public string? OrderStatuses { get; set; }
}

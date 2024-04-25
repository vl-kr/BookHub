namespace WebMVC.Helpers;

public class OrderStatusMapper
{
    public static readonly Dictionary<string, string> ColorMap =
        new()
        {
            { "New", "#3498db" },
            { "Closed", "#e74c3c" },
            { "Cancelled", "#f39c12" },
            { "Payment Received", "#27ae60" },
            { "Payment Failed", "#c0392b" },
            { "In Progress", "#f1c40f" },
            { "Completed", "#2ecc71" }
        };
}

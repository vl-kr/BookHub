namespace BusinessLayer.DTOs.Requests.ShippingMethod;

public class ShippingMethodRequest : BaseRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

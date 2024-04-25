namespace BusinessLayer.DTOs.Requests.PaymentMethod;

public class PaymentMethodRequest : BaseRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

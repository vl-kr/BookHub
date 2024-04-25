namespace BusinessLayer.DTOs.Responses.PaymentMethod;

public class PaymentMethodResponse : BaseResponse
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

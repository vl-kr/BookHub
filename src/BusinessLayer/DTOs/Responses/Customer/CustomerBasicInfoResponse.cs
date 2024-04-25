namespace BusinessLayer.DTOs.Responses.Customer;

public class CustomerBasicInfoResponse : BaseResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}

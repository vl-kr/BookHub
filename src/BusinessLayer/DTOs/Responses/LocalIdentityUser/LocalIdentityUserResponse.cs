using BusinessLayer.DTOs.Responses.Customer;

namespace BusinessLayer.DTOs.Responses.LocalIdentityUser;

public class LocalIdentityUserResponse : BaseResponse
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public CustomerBasicInfoResponse? Customer { get; set; }
}

using BusinessLayer.DTOs.Responses.Address;
using BusinessLayer.DTOs.Responses.Order;

namespace BusinessLayer.DTOs.Responses.Customer;

public class CustomerResponse : BaseResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public IEnumerable<OrderBasicInfoResponse> Orders { get; set; } =
        new List<OrderBasicInfoResponse>();

    public int? ShoppingCartId { get; set; }
    public int? WishlistId { get; set; }
    public AddressResponse? PreferredShippingAddress { get; set; }
    public AddressResponse? PreferredBillingAddress { get; set; }
}

using BusinessLayer.DTOs.Responses.OrderStatus;

namespace BusinessLayer.DTOs.Responses.Order;

public class OrderBasicInfoResponse : BaseResponse
{
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public string? Note { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatusResponse? Status { get; set; }
}

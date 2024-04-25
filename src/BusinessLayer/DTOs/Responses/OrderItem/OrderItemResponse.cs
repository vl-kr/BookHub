using BusinessLayer.DTOs.Responses.Book;

namespace BusinessLayer.DTOs.Responses.OrderItem;

public class OrderItemResponse : BaseResponse
{
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public BookBasicInfoResponse? Book { get; set; }
}

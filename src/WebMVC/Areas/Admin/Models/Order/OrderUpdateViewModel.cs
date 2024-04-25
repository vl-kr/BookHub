using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Order;

public class OrderUpdateViewModel
{
    [Required]
    [Display(Name = "Order status")]
    public int OrderStatusId { get; set; }
}

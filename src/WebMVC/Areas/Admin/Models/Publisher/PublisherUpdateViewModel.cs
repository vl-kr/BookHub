using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Publisher;

public class PublisherUpdateViewModel
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; } = "";
}

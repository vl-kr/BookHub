using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Author;

public class AuthorUpdateViewModel
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; } = "";
}

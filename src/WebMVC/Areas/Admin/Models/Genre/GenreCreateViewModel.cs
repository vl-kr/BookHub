using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Genre;

public class GenreCreateViewModel
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; } = "";
}

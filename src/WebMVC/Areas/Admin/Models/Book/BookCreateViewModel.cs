using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Book;

public class BookCreateViewModel
{
    [Required]
    [DataType(DataType.Text)]
    public string Title { get; set; } = "";

    [Required]
    [DataType(DataType.Text)]
    public string Description { get; set; } = "";

    [Required]
    [DataType(DataType.Currency)]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Please enter a positive price")]
    public decimal Price { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$", ErrorMessage = "Please enter a valid ISBN-13")]
    public string ISBN { get; set; } = "";

    [Required]
    [Display(Name = "Year published")]
    [Range(0, 2024, ErrorMessage = "Please enter a valid year")]
    public int YearPublished { get; set; }

    [DataType(DataType.Url)]
    [Display(Name = "Image url")]
    [Url(ErrorMessage = "Please enter a valid URL")]
    public string? ImageUrl { get; set; } = "";

    [Required]
    [Display(Name = "Publisher")]
    public int PublisherId { get; set; }

    [Required]
    [Display(Name = "Primary genre")]
    public int PrimaryGenreId { get; set; }

    [Required]
    [Display(Name = "Authors")]
    public List<int> AuthorIds { get; set; } = new();

    [Required]
    [Display(Name = "Genres")]
    public List<int> GenreIds { get; set; } = new();
}

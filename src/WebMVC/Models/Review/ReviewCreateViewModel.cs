using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models.Review;

public class ReviewCreateViewModel
{
    [Required]
    public int Rating { get; set; }

    [DataType(DataType.Text)]
    public string? TextReview { get; set; }
}

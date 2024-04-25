using System.Net;

namespace WebMVC.Models.Shared;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public string? ErrorMessage { get; set; }
    public HttpStatusCode? StatusCode { get; set; }
}

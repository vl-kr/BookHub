using System.Text;
using Newtonsoft.Json;

namespace WebAPI.Middlewares;

public class TransformOutputMiddleware
{
    private readonly ILogger<TransformOutputMiddleware> _logger;
    private readonly RequestDelegate _next;

    public TransformOutputMiddleware(
        RequestDelegate next,
        ILogger<TransformOutputMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var queryParameters = context.Request.Query;

        if (
            !queryParameters.ContainsKey("responseFormat")
            || queryParameters["responseFormat"] != "xml"
        )
        {
            _logger.LogInformation("Response format missing or not specified as xml");
            await _next(context);
            return;
        }

        await ConvertJsonResponse(context);
    }

    private async Task ConvertJsonResponse(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        try
        {
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonResponse = await new StreamReader(responseBody).ReadToEndAsync();

            var xmlResponse = ConvertJsonToXml(jsonResponse);

            context.Response.ContentType = "application/xml";

            _logger.LogInformation("JSON parsed successfully");
            var xmlBytes = Encoding.UTF8.GetBytes(xmlResponse);
            await originalBodyStream.WriteAsync(xmlBytes);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private string ConvertJsonToXml(string jsonResponse)
    {
        var doc = JsonConvert.DeserializeXmlNode("{\"object\":" + jsonResponse + "}", "response");
        if (doc != null)
            return doc.OuterXml;

        _logger.LogInformation("Failed to parse JSON");
        throw new JsonSerializationException("Cannot parse JSON TO XML");
    }
}

using BusinessLayer.Enums;

namespace BusinessLayer.Services.Result;

public class ServiceResult<T>
{
    public ServiceResult(T data, ServiceResultCode statusCode = ServiceResultCode.OK)
    {
        Data = data;
        StatusCode = statusCode;
    }

    public ServiceResult(string? errorMessage, ServiceResultCode statusCode)
    {
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
    }

    public ServiceResult(ServiceResultCode statusCode)
    {
        StatusCode = statusCode;
    }

    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public ServiceResultCode StatusCode { get; set; }
}

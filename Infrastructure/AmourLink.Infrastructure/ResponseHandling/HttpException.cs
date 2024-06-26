using System.Net;

namespace AmourLink.Infrastructure.ResponseHandling;

public class HttpException : Exception
{
    public HttpException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
    public HttpException(HttpStatusCode statusCode, Dictionary<string, string> errorMessages,
        object? result = null,ResponseType responseType = ResponseType.ValidationFailed)
    {
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
        ResponseType = responseType;
        Result = result;
    }
    public HttpException(HttpStatusCode statusCode, string errorMessage,
        object? result = null, ResponseType responseType = ResponseType.HttpError)
    {
        StatusCode = statusCode;
        ErrorMessages.Add("default", errorMessage);
        ResponseType = responseType;
        Result = result;
    }
    
    public HttpStatusCode StatusCode { get; set; }
    public Dictionary<string, string> ErrorMessages { get; set; } = [];
    public ResponseType ResponseType { get; set; } = ResponseType.HttpError;
    public object? Result { get; set; }
}
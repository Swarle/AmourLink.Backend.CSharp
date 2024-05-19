using System.Net;

namespace AmourLink.Recommendation.Infrastructure;

public class HttpException : Exception
{
    public HttpException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
    public HttpException(HttpStatusCode statusCode, Dictionary<string, string> errorMessages,
        object? result = null,ResponseType responseType = ResponseType.ValidationError)
    {
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
        ResponseTypeValue = responseType;
        Result = result;
    }
    public HttpException(HttpStatusCode statusCode, string errorMessage,
        object? result = null, ResponseType responseType = ResponseType.HttpError)
    {
        StatusCode = statusCode;
        ErrorMessages.Add("default", errorMessage);
        ResponseTypeValue = responseType;
        Result = result;
    }
    
    public HttpStatusCode StatusCode { get; set; }
    public Dictionary<string, string> ErrorMessages { get; set; } = [];
    public ResponseType ResponseTypeValue { get; set; } = ResponseType.HttpError;
    public object? Result { get; set; }
}
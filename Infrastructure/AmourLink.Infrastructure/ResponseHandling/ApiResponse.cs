using System.Text.Json.Serialization;

namespace AmourLink.Infrastructure.ResponseHandling;

public class ApiResponse
{
    [JsonPropertyName("ResponseType")]
    public string ResponseTypeValue { get; set; } = ResponseType.Success.ToString();
    public object? Result { get; set; }
    public Dictionary<string, string> ErrorMessages { get; set; } = [];
    
    public ApiResponse()
    {
        
    }
    
    public ApiResponse(ResponseType responseType, string? errorMessage = null, object? result = null)
    {
        ResponseTypeValue = responseType.ToString();
        Result = result;
        
        if(!string.IsNullOrWhiteSpace(errorMessage))
            ErrorMessages.Add("default", errorMessage);
    }
    
    public ApiResponse(ResponseType responseType, Dictionary<string,string> errorMessages, object? result = null)
    {
        ResponseTypeValue = responseType.ToString();
        Result = result;
        ErrorMessages = errorMessages;
    }
    

    public static ApiResponse Success(object? result = null)
    {
        return new ApiResponse
        {
            Result = result
        };
    }
    public static ApiResponse HttpException(HttpException exception)
    {
        return new ApiResponse
        {
            ResponseTypeValue = exception.ResponseTypeValue.ToString(),
            Result = exception.Result,
            ErrorMessages = exception.ErrorMessages
        };
    }
    public static ApiResponse Exception(Exception exception)
    {
        var response =  new ApiResponse
        {
            ResponseTypeValue = ResponseType.HttpError.ToString(),
            ErrorMessages = new Dictionary<string, string>
            {
                {"default", exception.Message}
            }
        };

        if(exception.StackTrace is not null)
            response.ErrorMessages.Add("stackTrace", exception.StackTrace);
        
        return response;
    }
}


namespace AmourLink.Infrastructure.ResponseHandling;

public class ApiResponse
{
    public ResponseType ResponseType { get; set; } = ResponseType.Success;
    public object? Result { get; set; }
    public Dictionary<string, string> ErrorMessages { get; init; } = [];
    
    public ApiResponse()
    {
        
    }
    
    public ApiResponse(ResponseType responseType, string? errorMessage = null, object? result = null)
    {
        ResponseType = responseType;
        Result = result;
        
        if(!string.IsNullOrWhiteSpace(errorMessage))
            ErrorMessages.Add("default", errorMessage);
    }
    
    public ApiResponse(ResponseType responseType, Dictionary<string,string> errorMessages, object? result = null)
    {
        ResponseType = responseType;
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
            ResponseType = exception.ResponseType,
            Result = exception.Result,
            ErrorMessages = exception.ErrorMessages
        };
    }
    public static ApiResponse Exception(Exception exception)
    {
        var response =  new ApiResponse
        {
            ResponseType = ResponseType.HttpError,
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


namespace AmourLink.Infrastructure.ResponseHandling;

public class ApiResponse<TResult> : ApiResponse
{
    public new TResult? Result { get; set; }
}
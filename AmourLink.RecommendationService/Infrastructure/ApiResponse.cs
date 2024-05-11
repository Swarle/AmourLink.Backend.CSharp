namespace AmourLink.RecommendationService.Infrastructure;

public class ApiResponse
{
    public ResponseType ResponseType { get; set; }
    public object? Result { get; set; }
    public Dictionary<string, string> ErrorMessages { get; set; } = [];
}
using System.Net;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Services.Interfaces;

namespace AmourLink.Recommendation.Services;

public class SwipeHttpService : ISwipeHttpService
{
    private readonly HttpClient _httpClient;

    public SwipeHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<InteractionDto> GetInteractionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var requestUri =$"api/swipe-service/interactions?userId={userId}";
        
        var response = await _httpClient.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => new HttpException(HttpStatusCode.Unauthorized),
                _ => new ArgumentOutOfRangeException(nameof(response.StatusCode))
            };
        }

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<InteractionDto>>(cancellationToken) ??
            throw new NullReferenceException($"Response body from {_httpClient.BaseAddress + requestUri} is null");
        
        if(result.Result == null)
            throw new NullReferenceException($"Body result from {_httpClient.BaseAddress + requestUri} is null");

        return result.Result;
    }
}
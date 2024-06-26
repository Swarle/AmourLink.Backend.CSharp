using AmourLink.Infrastructure.Helpers;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Swipe.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AmourLink.Swipe.Services;

public class MatchHttpService : IMatchHttpService
{
    private readonly HttpClient _httpClient;

    public MatchHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ICollection<Guid>> GetMatchedUsersId(Guid userId, CancellationToken cancellationToken = default)
    {
        var requestUri = $"api/match-service/match?userId={userId}";

        var response = await _httpClient.GetAsync(requestUri, cancellationToken);

        response.EnsureSuccessStatusCode();
        
        var serializerSettings = new JsonSerializerSettings();
        
        serializerSettings.Converters.Add(new StringEnumConverter(new UpperCaseNamingStrategy()));

        var resultJson = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = JsonConvert.DeserializeObject<ApiResponse<ICollection<Guid>>>(resultJson, serializerSettings) ??
                     throw new NullReferenceException($"Response body from {_httpClient.BaseAddress + requestUri} is null");
        
        if(result.Result == null)
            throw new NullReferenceException($"Body result from {_httpClient.BaseAddress + requestUri} is null");

        return result.Result;
    }
}
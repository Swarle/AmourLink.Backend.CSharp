using System.Text.Json;

namespace AmourLink.Recommendation.Pagination;

public static class PaginationExtension
{
    public static void AddPaginationHeader(this HttpResponse response, int currentPage,
        int totalPages, int totalCount)
    {
        var paginationHeader = new PaginationHeader(currentPage, totalPages, totalCount);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(paginationHeader, options);
        
        response.Headers.Append("Pagination", json);
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}
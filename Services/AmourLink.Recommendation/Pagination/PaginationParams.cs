using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AmourLink.Recommendation.Pagination;

public class PaginationParams
{
    public int PageNumber { get; set; } = 1;
    
    [BindNever]
    public int PageSize => 1;
}
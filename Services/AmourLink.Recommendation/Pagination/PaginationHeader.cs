namespace AmourLink.Recommendation.Pagination;

public class PaginationHeader
{
    public PaginationHeader(int currentPage, int totalPages, int totalCount)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        TotalCount = totalCount;
    }
    
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}
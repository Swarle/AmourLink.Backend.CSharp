namespace AmourLink.Recommendation.Infrastructure.Pagination;

public class PagedEntity<TEntity> : IPaged
{
    public PagedEntity(TEntity? result, int count, int pageNumber)
    {
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) PageSize);
        TotalCount = count;
        Result = result;
    }
    public TEntity? Result { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 1;
    public int TotalCount { get; set; }
}
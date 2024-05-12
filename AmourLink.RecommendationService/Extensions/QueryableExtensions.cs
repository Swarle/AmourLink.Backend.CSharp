using AmourLink.RecommendationService.Data.Abstract;
using AmourLink.RecommendationService.Specification.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> inputQuery,
        BaseSpecification<TEntity> specification) where TEntity : Entity
    {
        var query = inputQuery;
            
        if (specification.Expression != null)
        {
            query = query.Where(specification.Expression);
        }

        if (specification.IncludeExpressions.Count > 0)
        {
            query = specification.IncludeExpressions.Aggregate(query, (current, include) =>
                current.Include(include));
        }

        if (specification.IncludeString.Count > 0)
        {
            query = specification.IncludeString
                .Aggregate(query,
                    (current, include) => current.Include(include));
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderByDescending(specification.OrderBy);
        }
        
        return query;
    }
}
using AmourLink.Recommendation.Data.Abstract;
using AmourLink.Recommendation.Specification.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.Recommendation.Extensions;

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
            query = specification.IsOrderByDescending ? query.OrderByDescending(specification.OrderBy) 
                : query.OrderBy(specification.OrderBy);
        }
        
        return query;
    }
}
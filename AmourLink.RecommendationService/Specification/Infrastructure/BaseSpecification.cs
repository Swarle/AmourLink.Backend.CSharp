using System.Linq.Expressions;
using AmourLink.RecommendationService.Data.Abstract;
using AmourLink.RecommendationService.Infrastructure;

namespace AmourLink.RecommendationService.Specification.Infrastructure;

public abstract class BaseSpecification<TEntity> where TEntity : Entity
{
    public Expression<Func<TEntity, bool>>? Expression { get; private set; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];
    public List<string> IncludeString { get; private set; } = [];
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }


    protected BaseSpecification(Expression<Func<TEntity, bool>> expression)
    {
        Expression = expression;
    }

    protected BaseSpecification()
    {
        
    }

    protected virtual void AddExpression(Expression<Func<TEntity, bool>> expression)
    {
        Expression = Expression == null ? expression 
            : Expression.And(expression);
    }

    protected virtual void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected virtual void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        IncludeExpressions.Add(expression);
    }

    protected virtual void AddInclude(string include)
    {
        IncludeString.Add(include);
    }
        
    public virtual bool IsSatisfied(TEntity obj)
    {
        bool result = Expression!.Compile().Invoke(obj);

        return result;
    }

}
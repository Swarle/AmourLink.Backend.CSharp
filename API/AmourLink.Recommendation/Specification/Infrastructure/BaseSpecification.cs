using System.Linq.Expressions;
using AmourLink.Recommendation.Data.Abstract;
using AmourLink.Recommendation.Infrastructure;

namespace AmourLink.Recommendation.Specification.Infrastructure;

public abstract class BaseSpecification<TEntity> where TEntity : Entity
{
    public Expression<Func<TEntity, bool>>? Expression { get; private set; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];
    public List<string> IncludeString { get; private set; } = [];
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? ThenBy { get; private set; }
    public bool IsOrderByDescending { get; private set; } = false;
    public bool IsThenByDescending { get; private set; } = false;


    protected BaseSpecification(Expression<Func<TEntity, bool>> expression)
    {
        Expression = expression;
    }

    protected BaseSpecification()
    {
        
    }

    protected void AddExpression(Expression<Func<TEntity, bool>> expression)
    {
        Expression = Expression == null ? expression 
            : Expression.And(expression);
    }

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isDescending = false)
    {
        OrderBy = orderByExpression;
        IsOrderByDescending = isDescending;
    }

    protected void AddThenBy(Expression<Func<TEntity, object>> thenByExpression, bool isDescending = false)
    {
        ThenBy = thenByExpression;
        IsThenByDescending = isDescending;
    }

    protected void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        IncludeExpressions.Add(expression);
    }

    protected void AddInclude(string include)
    {
        IncludeString.Add(include);
    }
    

}
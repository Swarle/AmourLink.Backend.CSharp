using AmourLink.Recommendation.Data.Abstract;
using AmourLink.Recommendation.Specification.Infrastructure;

namespace AmourLink.Recommendation.Repository;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(BaseSpecification<TEntity> specification,
        CancellationToken cancellationToken = default);
}
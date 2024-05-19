using AmourLink.Recommendation.Data.Abstract;
using AmourLink.Recommendation.Infrastructure.Pagination;
using AmourLink.Recommendation.Specification.Infrastructure;

namespace AmourLink.Recommendation.Repository;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(BaseSpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<PagedList<TEntity>> GetPagedListAsync(BaseSpecification<TEntity> specification, int? pageNumber,
        int? pageSize = null, CancellationToken cancellationToken = default);
    
    Task<PagedEntity<TEntity>> GetPagedEntityAsync(BaseSpecification<TEntity> specification, int? pageNumber,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetFirstOrDefaultAsync(BaseSpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

}
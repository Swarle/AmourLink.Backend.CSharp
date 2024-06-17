using AmourLink.Infrastructure.Data.Abstract;
using AmourLink.Infrastructure.Pagination;
using AmourLink.Infrastructure.Specification;

namespace AmourLink.Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllAsync(BaseSpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<PagedList<TEntity>> GetPagedListAsync(BaseSpecification<TEntity> specification, int? pageNumber,
        int? pageSize = 1, CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetFirstOrDefaultAsync(BaseSpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();

}
using AmourLink.Recommendation.Data.Abstract;
using AmourLink.Recommendation.Data.Context;
using AmourLink.Recommendation.Extensions;
using AmourLink.Recommendation.Infrastructure.Pagination;
using AmourLink.Recommendation.Specification.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.Recommendation.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity 
{
    private readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;

        return await query.ApplySpecification(specification).ToListAsync(cancellationToken);
    }

    public async Task<PagedList<TEntity>> GetPagedListAsync(BaseSpecification<TEntity> specification, int? pageNumber = 1, int? pageSize = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet;
        
        var totalCollectionCount = await query.ApplySpecification(specification).CountAsync(cancellationToken);

        var page = pageNumber ?? 1;
        var size = pageSize ?? totalCollectionCount;

        query = query.ApplySpecification(specification)
            .Skip((page - 1) * size)
            .Take(size);
        
        var data = await query.ToListAsync(cancellationToken);

        return new PagedList<TEntity>(data, totalCollectionCount, page, size);
    }
}
using AmourLink.Recommendation.Data.Abstract;
using AmourLink.Recommendation.Data.Context;
using AmourLink.Recommendation.Extensions;
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
}
using AmourLink.RecommendationService.Data.Abstract;
using AmourLink.RecommendationService.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Repository;

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
        return await DbSet.FindAsync(id, cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }
}
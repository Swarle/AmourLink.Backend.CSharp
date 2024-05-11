using AmourLink.RecommendationService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Data.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
}
using System.Reflection;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Recommendation.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace AmourLink.Recommendation.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))!);

        modelBuilder.SetValueConverterForGuids();
        modelBuilder.SetColumnTypeForGuids();
    }
}


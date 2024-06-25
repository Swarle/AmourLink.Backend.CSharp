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
    
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Info> Info { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.SetDefaultModelBuilder<ApplicationDbContext>();
        
        
    }
}


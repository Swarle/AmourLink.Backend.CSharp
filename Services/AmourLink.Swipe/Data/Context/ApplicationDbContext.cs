using AmourLink.Infrastructure.Extensions;
using AmourLink.Swipe.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.Swipe.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Like> Likes { get; set; }
    public DbSet<Interaction> Interactions { get; set; }
    
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
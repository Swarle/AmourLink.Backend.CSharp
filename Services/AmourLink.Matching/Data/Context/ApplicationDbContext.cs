using System.Reflection;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Matching.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.Matching.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Match> Matches { get; set; }
    
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
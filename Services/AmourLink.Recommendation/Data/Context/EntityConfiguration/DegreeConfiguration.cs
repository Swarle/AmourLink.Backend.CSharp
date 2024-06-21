using AmourLink.Recommendation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
{
    public void Configure(EntityTypeBuilder<Degree> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");
        
        builder.ToTable("degree");
        
        builder.Property(e => e.Id)
            .HasColumnName("degree_id");
        builder.Property(e => e.DegreeName)
            .HasMaxLength(45);
        builder.Property(e => e.SchoolName)
            .HasMaxLength(100);
        builder.Property(e => e.StartYear)
            .HasColumnType("datetime");
    }
}
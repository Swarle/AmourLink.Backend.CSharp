using AmourLink.RecommendationService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.RecommendationService.Data.Context.EntityConfiguration;

public class HobbieConfiguration : IEntityTypeConfiguration<Hobbie>
{
    public void Configure(EntityTypeBuilder<Hobbie> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");
        
        builder.ToTable("hobbie");
        
        builder.Property(e => e.Id)
            .HasColumnName("hobbie_id")
            .HasColumnType("binary(16)");
        builder.Property(e => e.HobbieName)
            .HasMaxLength(45);
        builder.Property(e => e.UserDetailsId)
            .HasColumnType("binary(16)");;

        builder.HasOne(d => d.UserDetails)
            .WithMany(p => p.Hobbies)
            .HasForeignKey(d => d.UserDetailsId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_user_account_hobbie_user_details1");
    }
}
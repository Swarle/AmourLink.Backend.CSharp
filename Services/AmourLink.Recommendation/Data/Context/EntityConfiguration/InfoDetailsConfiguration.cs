using AmourLink.Recommendation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class InfoDetailsConfiguration : IEntityTypeConfiguration<InfoDetails>
{
    public void Configure(EntityTypeBuilder<InfoDetails> builder)
    {
        builder.HasKey(e => new { e.InfoId, e.AnswerId, e.UserId })
            .HasName("PRIMARY");

        builder.ToTable("info_details");
        
        builder.Property(e => e.InfoId)
            .HasColumnName("info_id");
        builder.Property(e => e.AnswerId)
            .HasColumnName("answer_id");
        builder.Property(e => e.UserId)
            .HasColumnName("user_id");

        builder.HasOne(e => e.Info)
            .WithMany(e => e.InfoDetails)
            .HasForeignKey(e => e.InfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.InfoAnswer)
            .WithMany(e => e.InfoDetails)
            .HasForeignKey(e => e.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.UserDetails)
            .WithMany(e => e.InfoDetails)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using AmourLink.Recommendation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class InfoAnswerConfiguration : IEntityTypeConfiguration<InfoAnswer>
{
    public void Configure(EntityTypeBuilder<InfoAnswer> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");
        
        builder.ToTable("info_answer");

        builder.Property(e => e.Id)
            .HasColumnName("answer_id");

        builder.HasOne(e => e.Info)
            .WithMany(e => e.Answers)
            .HasForeignKey(e => e.InfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
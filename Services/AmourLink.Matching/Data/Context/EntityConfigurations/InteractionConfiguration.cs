using AmourLink.Matching.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Matching.Data.Context.EntityConfigurations;

public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
{
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");

        builder.Property(e => e.Id)
            .HasColumnName("interaction_id");

        builder.Property(e => e.LastInteraction)
            .HasColumnType("timestamp");
    }
}
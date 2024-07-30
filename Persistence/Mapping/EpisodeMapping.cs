using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class EpisodeMapping : EntityTypeConfiguration<Episode>
{
    #region Configures

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<Episode> entity)
    {
        entity.ToTable(nameof(Episode), "public");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        entity.Property(e => e.OrderNumber).IsRequired();

        entity.Property(_ => _.IsDeleted).IsRequired().HasDefaultValue(false);
        entity.Property(_ => _.CreatedById).IsRequired();
        entity.Property(_ => _.CreatedByName).IsRequired().HasMaxLength(256);
        entity.Property(_ => _.CreatedDateTime).IsRequired();
        entity.Property(_ => _.UpdateById).IsRequired(false);
        entity.Property(_ => _.UpdatedByName).IsRequired(false).HasMaxLength(256);
        entity.Property(_ => _.UpdatedTime).IsRequired(false);

        entity.HasOne(e => e.Story)
            .WithMany(s => s.Episodes)
            .HasForeignKey(_ => _.StoryId);

        entity.HasOne(e => e.Audio)
            .WithOne(a => a.Episode)
            .HasForeignKey<Audio>(a => a.EpisodeId);
        
        #endregion
        
        #region Seeding data
        
        #endregion

        base.Configure(entity);
    }

}
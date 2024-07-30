using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class StoryMapping : EntityTypeConfiguration<Story>
{

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<Story> entity)
    {
        #region Configures

        entity.ToTable(nameof(Story), "public");

        entity.HasKey(s => s.Id);

        entity.Property(s => s.Id)
            .ValueGeneratedOnAdd();
        
        entity.Property(s => s.Name).IsRequired().HasMaxLength(256);

        entity.Property(s => s.Description).IsRequired(false);

        entity.Property(s => s.Rating).HasDefaultValue(0).IsRequired();

        entity.Property(s => s.Thumbnail).IsRequired(false);

        entity.Property(s => s.SourceDescription).IsRequired(false);

        entity.Property(s => s.Author).IsRequired(false);

        entity.Property(s => s.Voice).IsRequired(false);

        entity.Property(s => s.IsBook).IsRequired();

        entity.Property(s => s.IsStory).IsRequired();
        
        entity.Property(_ => _.IsDeleted).IsRequired().HasDefaultValue(false);
        entity.Property(_ => _.CreatedById).IsRequired();
        entity.Property(_ => _.CreatedByName).IsRequired().HasMaxLength(256);
        entity.Property(_ => _.CreatedDateTime).IsRequired();
        entity.Property(_ => _.UpdateById).IsRequired(false);
        entity.Property(_ => _.UpdatedByName).IsRequired(false).HasMaxLength(256);
        entity.Property(_ => _.UpdatedTime).IsRequired(false);

        entity.HasMany(s => s.Episodes)
            .WithOne(e => e.Story)
            .HasForeignKey(e => e.StoryId);
        
        entity.HasMany(s => s.Wishlists)
            .WithOne(st => st.Story)
            .HasForeignKey(st => st.StoryId);
        #endregion

        #region Seeding data
        #endregion

        base.Configure(entity);
    }

}
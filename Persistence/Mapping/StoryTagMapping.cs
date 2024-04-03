using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class StoryTagMapping : EntityTypeConfiguration<StoryTag>
{
    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<StoryTag> entity)
    {
        #region Configures

        entity.ToTable(nameof(Tag), "public");

        entity.HasKey(st => new {st.StoryId, st.TagId});

        entity.HasOne(st => st.Tag)
            .WithMany(t => t.StoryTags)
            .HasForeignKey(st => st.TagId);

        entity.HasOne(st => st.Story)
            .WithMany(s => s.StoryTags)
            .HasForeignKey(st => st.StoryId);
        
        #endregion

        base.Configure(entity);
    }
}
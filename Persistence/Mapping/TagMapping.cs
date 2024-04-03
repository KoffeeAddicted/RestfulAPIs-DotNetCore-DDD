using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class TagMapping : EntityTypeConfiguration<Tag>
{

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<Tag> entity)
    {
        #region Configures

        entity.ToTable(nameof(Tag), "public");

        entity.HasKey(sc => sc.Id);

        entity.Property(sc => sc.Name).IsRequired();

        entity.Property(sc => sc.IsDeleted).IsRequired().HasDefaultValue(false);

        entity.HasMany(t => t.StoryTags)
            .WithOne(st => st.Tag)
            .HasForeignKey(st => st.TagId);
        
        #endregion
        

        base.Configure(entity);
    }
}
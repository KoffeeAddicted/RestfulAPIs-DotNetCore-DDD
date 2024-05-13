using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class CommnentMapping : EntityTypeConfiguration<Comment>
{
    #region Configures

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<Comment> entity)
    {
        entity.ToTable(nameof(Comment), "public");

        entity.HasKey(st => new { st.StoryId, st.ProviderToken });

        entity.HasOne(st => st.User)
            .WithMany(t => t.Comments)
            .HasForeignKey(st => st.ProviderToken);

        entity.HasOne(st => st.Story)
            .WithMany(s => s.Comments)
            .HasForeignKey(st => st.StoryId);
        #endregion

        #region Seeding data

        #endregion

        base.Configure(entity);
    }

}
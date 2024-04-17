using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class HistoryMapping : EntityTypeConfiguration<History>
{
    #region Configures

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<History> entity)
    {
        entity.ToTable(nameof(History), "public");

        entity.HasKey(st => new { st.StoryId, st.ProviderToken });

        entity.HasOne(st => st.User)
            .WithMany(t => t.Histories)
            .HasForeignKey(st => st.ProviderToken);

        entity.HasOne(st => st.Story)
            .WithMany(s => s.Histories)
            .HasForeignKey(st => st.StoryId);
        #endregion

        #region Seeding data

        #endregion

        base.Configure(entity);
    }

}

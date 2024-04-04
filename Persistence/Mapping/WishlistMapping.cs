using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class WishlistMapping : EntityTypeConfiguration<Wishlist>
{
    #region Configures

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<Wishlist> entity)
    {
        entity.ToTable(nameof(Wishlist), "public");

        entity.HasKey(st => new { st.StoryId, st.ProviderToken });

        entity.HasOne(st => st.User)
            .WithMany(t => t.Wishlists)
            .HasForeignKey(st => st.ProviderToken);

        entity.HasOne(st => st.Story)
            .WithMany(s => s.Wishlists)
            .HasForeignKey(st => st.StoryId);
        #endregion

        #region Seeding data

        #endregion

        base.Configure(entity);
    }

}
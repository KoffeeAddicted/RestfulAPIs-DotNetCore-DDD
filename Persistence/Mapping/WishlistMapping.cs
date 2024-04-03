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

        entity.HasKey(s => s.Id);

        entity.Property(s => s.Id)
            .ValueGeneratedOnAdd();

       

        entity.HasMany(sc => sc.User).(e => e.)
            .HasForeignKey(e => e.StoryId);

        entity.HasOne(s => s.StoryCategory)
            .WithMany(sc => sc.Stories)
            .HasForeignKey(s => s.StoryCategoryId);
        #endregion

        #region Seeding data

        #endregion

        base.Configure(entity);
    }

}
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class BannerMapping: EntityTypeConfiguration<Banner>
{
    #region Configures

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<Banner> entity)
    {

        entity.ToTable(nameof(Banner), "public");

        entity.HasKey(p => p.Id);

        #endregion

        #region Seeding data
        #endregion

        base.Configure(entity);
    }
}
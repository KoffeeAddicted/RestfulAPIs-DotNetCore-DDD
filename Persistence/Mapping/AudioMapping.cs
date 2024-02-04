using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class AudioMapping : EntityTypeConfiguration<Audio>
{
    #region Configures

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<Audio> entity)
    {
        entity.ToTable(nameof(Audio), "public");

        entity.HasKey(a => a.Id);

        entity.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        entity.Property(a => a.Link).IsRequired();

        entity.Property(_ => _.IsDeleted).IsRequired().HasDefaultValue(false);

        entity.HasOne(a => a.Episode)
            .WithOne(e => e.Audio)
            .HasForeignKey<Audio>(a => a.EpisodeId);
        
        #endregion
        
        #region Seeding data
        entity.HasData(
            new List<Audio>()
            {
                new Audio()
                {
                    Id = 1,
                    Link = "123",
                    EpisodeId = 1
                },
                new Audio()
                {
                    Id = 2,
                    Link = "456",
                    EpisodeId = 2
                },
            }
        );
        #endregion

        base.Configure(entity);
    }

}
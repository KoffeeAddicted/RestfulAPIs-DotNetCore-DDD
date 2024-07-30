using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mapping;

public class StoryCategoryMapping : EntityTypeConfiguration<StoryCategory>
{
    #region Configures

    /// <summary>
    /// Configures the entity
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity</param>
    /// <param name="entity"></param>
    public override void Configure(EntityTypeBuilder<StoryCategory> entity)
    {
        entity.ToTable(nameof(StoryCategory), "public");

        entity.HasKey(sc => sc.Id);

        entity.Property(sc => sc.Name).IsRequired();

        entity.Property(sc => sc.IsDeleted).IsRequired().HasDefaultValue(false);
        #endregion
        
        #region Seeding data
        entity.HasData(
            new List<StoryCategory>()
            {
                new StoryCategory()
                {
                    Id = 1,
                    Name = "Kinh dị"
                },
                new StoryCategory()
                {
                    Id = 2,
                    Name = "Hài"
                }
            }
        );
        #endregion

        base.Configure(entity);
    }

}
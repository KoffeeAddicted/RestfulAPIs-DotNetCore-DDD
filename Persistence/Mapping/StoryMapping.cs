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

        entity.HasOne(s => s.StoryCategory)
            .WithMany(sc => sc.Stories)
            .HasForeignKey(s => s.StoryCategoryId);

        entity.HasMany(s => s.Wishlists)
            .WithOne(st => st.Story)
            .HasForeignKey(st => st.StoryId);
        #endregion

        #region Seeding data
        entity.HasData(
            new Story()
            {
                Id = 1,
                Name = "Truyện ma rợn gáy về Ma Da miền sông nước",
                Description = "Câu chuyện về một làng chài nhỏ ở Nha Trang, nơi ẩn chứa những ký ức kinh hoàng, những khoánh khắc rùng rợn về loài ma đáng sợ: Ma da, trên những chuyến hải trình dài ngoài biển khơi....\n\nMời các bạn đón nghe chuyện ma kinh dị  (phần 1/2) của tác giả Nguyễn Quốc Huy (Huy Rùi) qua giọng đọc Tả Từ. Các bạn nên nghe bằng tai nghe để có trải nghiệm tốt nhất. Nếu cảm thấy thú vị, các bạn có thể sử dụng tính năng SuperThank (\"Cảm ơn\"), nút ở dưới các video để tặng cho MC một cốc cafe. Trân trọng!",
                Rating = 8.5,
                Author = "Bí ẩn radio",
                Voice = "MC tả từ",
                IsStory = true,
                IsBook = false,
                StoryCategoryId = 1,
                CreatedById = 1,
                CreatedByName = "System"
            }
        );
        #endregion

        base.Configure(entity);
    }

}
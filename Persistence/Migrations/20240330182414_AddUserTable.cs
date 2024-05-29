using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "StoryCategory",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProviderToken = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Thumbnail = table.Column<string>(type: "text", nullable: true),
                    SourceDescription = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Voice = table.Column<string>(type: "text", nullable: true),
                    IsBook = table.Column<bool>(type: "boolean", nullable: false),
                    IsStory = table.Column<bool>(type: "boolean", nullable: false),
                    StoryCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedByName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    UpdateById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Story_StoryCategory_StoryCategoryId",
                        column: x => x.StoryCategoryId,
                        principalSchema: "public",
                        principalTable: "StoryCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episode",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderNumber = table.Column<int>(type: "integer", nullable: false),
                    StoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedByName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    UpdateById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episode_Story_StoryId",
                        column: x => x.StoryId,
                        principalSchema: "public",
                        principalTable: "Story",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Audio",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EpisodeId = table.Column<long>(type: "bigint", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audio_Episode_EpisodeId",
                        column: x => x.EpisodeId,
                        principalSchema: "public",
                        principalTable: "Episode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "StoryCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Kinh dị" },
                    { 2L, "Hài" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "User",
                columns: new[] { "Id", "ProviderToken" },
                values: new object[] { 1L, "123" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Story",
                columns: new[] { "Id", "Author", "CreatedById", "CreatedByName", "CreatedDateTime", "Description", "IsBook", "IsStory", "Name", "Rating", "SourceDescription", "StoryCategoryId", "Thumbnail", "UpdateById", "UpdatedByName", "UpdatedTime", "Voice" },
                values: new object[] { 1L, "Bí ẩn radio", 1L, "System", new DateTime(2024, 3, 30, 18, 24, 13, 593, DateTimeKind.Utc).AddTicks(6983), "Câu chuyện về một làng chài nhỏ ở Nha Trang, nơi ẩn chứa những ký ức kinh hoàng, những khoánh khắc rùng rợn về loài ma đáng sợ: Ma da, trên những chuyến hải trình dài ngoài biển khơi....\n\nMời các bạn đón nghe chuyện ma kinh dị  (phần 1/2) của tác giả Nguyễn Quốc Huy (Huy Rùi) qua giọng đọc Tả Từ. Các bạn nên nghe bằng tai nghe để có trải nghiệm tốt nhất. Nếu cảm thấy thú vị, các bạn có thể sử dụng tính năng SuperThank (\"Cảm ơn\"), nút ở dưới các video để tặng cho MC một cốc cafe. Trân trọng!", false, true, "Truyện ma rợn gáy về Ma Da miền sông nước", 8.5, null, 1L, null, null, null, null, "MC tả từ" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Episode",
                columns: new[] { "Id", "CreatedById", "CreatedByName", "CreatedDateTime", "OrderNumber", "StoryId", "UpdateById", "UpdatedByName", "UpdatedTime" },
                values: new object[,]
                {
                    { 1L, 1L, "System", new DateTime(2024, 3, 30, 18, 24, 13, 592, DateTimeKind.Utc).AddTicks(9157), 1, 1L, null, null, null },
                    { 2L, 1L, "System", new DateTime(2024, 3, 30, 18, 24, 13, 592, DateTimeKind.Utc).AddTicks(9167), 2, 1L, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Audio",
                columns: new[] { "Id", "Duration", "EpisodeId", "Link" },
                values: new object[,]
                {
                    { 1L, 0L, 1L, "123" },
                    { 2L, 0L, 2L, "456" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audio_EpisodeId",
                schema: "public",
                table: "Audio",
                column: "EpisodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episode_StoryId",
                schema: "public",
                table: "Episode",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_StoryCategoryId",
                schema: "public",
                table: "Story",
                column: "StoryCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audio",
                schema: "public");

            migrationBuilder.DropTable(
                name: "User",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Episode",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Story",
                schema: "public");

            migrationBuilder.DropTable(
                name: "StoryCategory",
                schema: "public");
        }
    }
}

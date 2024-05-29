using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWishlistandTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "public",
                table: "User");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "User",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "public",
                table: "User",
                column: "ProviderToken");

            migrationBuilder.CreateTable(
                name: "Tag",
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
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                schema: "public",
                columns: table => new
                {
                    ProviderToken = table.Column<string>(type: "text", nullable: false),
                    StoryId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedByName = table.Column<string>(type: "text", nullable: true),
                    UpdateById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => new { x.StoryId, x.ProviderToken });
                    table.ForeignKey(
                        name: "FK_Wishlist_Story_StoryId",
                        column: x => x.StoryId,
                        principalSchema: "public",
                        principalTable: "Story",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlist_User_ProviderToken",
                        column: x => x.ProviderToken,
                        principalSchema: "public",
                        principalTable: "User",
                        principalColumn: "ProviderToken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryTag",
                schema: "public",
                columns: table => new
                {
                    StoryId = table.Column<long>(type: "bigint", nullable: false),
                    TagId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryTag", x => new { x.StoryId, x.TagId });
                    table.ForeignKey(
                        name: "FK_StoryTag_Story_StoryId",
                        column: x => x.StoryId,
                        principalSchema: "public",
                        principalTable: "Story",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "public",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 4, 3, 24, 0, 430, DateTimeKind.Utc).AddTicks(1621));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 4, 3, 24, 0, 430, DateTimeKind.Utc).AddTicks(1626));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Story",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 4, 3, 24, 0, 430, DateTimeKind.Utc).AddTicks(7012));

            migrationBuilder.InsertData(
                schema: "public",
                table: "User",
                columns: new[] { "ProviderToken", "Email", "Id", "IsAdmin", "Password", "ProfilePicture" },
                values: new object[] { "123", null, 1L, false, null, "picture.com" });

            migrationBuilder.CreateIndex(
                name: "IX_StoryTag_TagId",
                schema: "public",
                table: "StoryTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_ProviderToken",
                schema: "public",
                table: "Wishlist",
                column: "ProviderToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryTag",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Wishlist",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "public",
                table: "User");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "User",
                keyColumn: "ProviderToken",
                keyValue: "123");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "public",
                table: "User",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 3, 31, 9, 39, 6, 300, DateTimeKind.Utc).AddTicks(2762));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 3, 31, 9, 39, 6, 300, DateTimeKind.Utc).AddTicks(2767));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Story",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 3, 31, 9, 39, 6, 300, DateTimeKind.Utc).AddTicks(8131));

            migrationBuilder.InsertData(
                schema: "public",
                table: "User",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "ProfilePicture", "ProviderToken" },
                values: new object[] { 1L, null, false, null, "picture.com", "123" });
        }
    }
}

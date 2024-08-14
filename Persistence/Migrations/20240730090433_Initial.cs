using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

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
                    StoryCategoryId = table.Column<long[]>(type: "bigint[]", nullable: false),
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
                });

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
                    ProviderToken = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    ProfilePicture = table.Column<string>(type: "text", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ProviderToken);
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
                name: "Comment",
                schema: "public",
                columns: table => new
                {
                    ProviderToken = table.Column<string>(type: "text", nullable: false),
                    StoryId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Comment", x => new { x.StoryId, x.ProviderToken });
                    table.ForeignKey(
                        name: "FK_Comment_Story_StoryId",
                        column: x => x.StoryId,
                        principalSchema: "public",
                        principalTable: "Story",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_ProviderToken",
                        column: x => x.ProviderToken,
                        principalSchema: "public",
                        principalTable: "User",
                        principalColumn: "ProviderToken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "History",
                schema: "public",
                columns: table => new
                {
                    ProviderToken = table.Column<string>(type: "text", nullable: false),
                    StoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => new { x.StoryId, x.ProviderToken });
                    table.ForeignKey(
                        name: "FK_History_Story_StoryId",
                        column: x => x.StoryId,
                        principalSchema: "public",
                        principalTable: "Story",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_User_ProviderToken",
                        column: x => x.ProviderToken,
                        principalSchema: "public",
                        principalTable: "User",
                        principalColumn: "ProviderToken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                schema: "public",
                columns: table => new
                {
                    ProviderToken = table.Column<string>(type: "text", nullable: false),
                    StoryId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "Audio",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EpisodeId = table.Column<long>(type: "bigint", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
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

            migrationBuilder.CreateIndex(
                name: "IX_Audio_EpisodeId",
                schema: "public",
                table: "Audio",
                column: "EpisodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProviderToken",
                schema: "public",
                table: "Comment",
                column: "ProviderToken");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_StoryId",
                schema: "public",
                table: "Episode",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_History_ProviderToken",
                schema: "public",
                table: "History",
                column: "ProviderToken");

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
                name: "Audio",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Comment",
                schema: "public");

            migrationBuilder.DropTable(
                name: "History",
                schema: "public");

            migrationBuilder.DropTable(
                name: "StoryCategory",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Wishlist",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Episode",
                schema: "public");

            migrationBuilder.DropTable(
                name: "User",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Story",
                schema: "public");
        }
    }
}

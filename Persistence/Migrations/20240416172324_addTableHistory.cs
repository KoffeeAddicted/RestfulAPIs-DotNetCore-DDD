using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addTableHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 16, 17, 23, 24, 214, DateTimeKind.Utc).AddTicks(1103));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 16, 17, 23, 24, 214, DateTimeKind.Utc).AddTicks(1109));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Story",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 16, 17, 23, 24, 216, DateTimeKind.Utc).AddTicks(800));

            migrationBuilder.CreateIndex(
                name: "IX_History_ProviderToken",
                schema: "public",
                table: "History",
                column: "ProviderToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History",
                schema: "public");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 15, 17, 11, 34, 52, DateTimeKind.Utc).AddTicks(5595));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 15, 17, 11, 34, 52, DateTimeKind.Utc).AddTicks(5603));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Story",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 15, 17, 11, 34, 53, DateTimeKind.Utc).AddTicks(4145));
        }
    }
}

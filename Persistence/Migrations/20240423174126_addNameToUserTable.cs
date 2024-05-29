using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addNameToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "User",
                keyColumn: "ProviderToken",
                keyValue: "123");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 23, 17, 41, 26, 98, DateTimeKind.Utc).AddTicks(9718));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 23, 17, 41, 26, 98, DateTimeKind.Utc).AddTicks(9725));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Story",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 23, 17, 41, 26, 101, DateTimeKind.Utc).AddTicks(688));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "public",
                table: "User");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 20, 8, 16, 19, 84, DateTimeKind.Utc).AddTicks(7140));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 20, 8, 16, 19, 84, DateTimeKind.Utc).AddTicks(7149));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Story",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 4, 20, 8, 16, 19, 86, DateTimeKind.Utc).AddTicks(8669));

            migrationBuilder.InsertData(
                schema: "public",
                table: "User",
                columns: new[] { "ProviderToken", "Email", "Id", "IsAdmin", "Password", "ProfilePicture" },
                values: new object[] { "123", null, 1L, false, null, "picture.com" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                schema: "public",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true);

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

            migrationBuilder.UpdateData(
                schema: "public",
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Email", "IsAdmin", "Password", "ProfilePicture" },
                values: new object[] { null, false, null, "picture.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "public",
                table: "User");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 3, 30, 18, 24, 13, 592, DateTimeKind.Utc).AddTicks(9157));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Episode",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 3, 30, 18, 24, 13, 592, DateTimeKind.Utc).AddTicks(9167));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Story",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedDateTime",
                value: new DateTime(2024, 3, 30, 18, 24, 13, 593, DateTimeKind.Utc).AddTicks(6983));
        }
    }
}

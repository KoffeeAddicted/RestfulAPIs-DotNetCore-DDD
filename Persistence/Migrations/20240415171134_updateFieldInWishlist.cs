using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateFieldInWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "public",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                schema: "public",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "public",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "public",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "public",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "UpdateById",
                schema: "public",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                schema: "public",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                schema: "public",
                table: "Wishlist");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                schema: "public",
                table: "Wishlist",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                schema: "public",
                table: "Wishlist",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "public",
                table: "Wishlist",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "public",
                table: "Wishlist",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "public",
                table: "Wishlist",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "UpdateById",
                schema: "public",
                table: "Wishlist",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                schema: "public",
                table: "Wishlist",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                schema: "public",
                table: "Wishlist",
                type: "timestamp with time zone",
                nullable: true);

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
        }
    }
}

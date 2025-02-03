using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_6_PhotoFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2591aa82-8df4-4f85-b904-260d042ef449");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea678235-e29d-4996-94ee-d73aac6fe83f");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bac45991-1203-482c-a078-def8c3ce0b97", null, "Admin", "ADMIN" },
                    { "e58010c3-42a3-433a-a48e-7ca3eb939df7", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bac45991-1203-482c-a078-def8c3ce0b97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e58010c3-42a3-433a-a48e-7ca3eb939df7");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Photos");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2591aa82-8df4-4f85-b904-260d042ef449", null, "Customer", "CUSTOMER" },
                    { "ea678235-e29d-4996-94ee-d73aac6fe83f", null, "Admin", "ADMIN" }
                });
        }
    }
}

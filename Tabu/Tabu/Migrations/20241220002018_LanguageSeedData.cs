using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tabu.Migrations
{
    /// <inheritdoc />
    public partial class LanguageSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Code", "Icon", "Name" },
                values: new object[,]
                {
                    { "az", "https://cdn-icons-png.flaticon.com/512/630/630657.png", "Azərbaycan" },
                    { "en", "https://cdn-icons-png.flaticon.com/512/5111/5111640.png", "English" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "az");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Code",
                keyValue: "en");
        }
    }
}

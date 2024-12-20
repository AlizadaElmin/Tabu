using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabu.Migrations
{
    /// <inheritdoc />
    public partial class GameTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BannedWordCount = table.Column<int>(type: "int", nullable: false),
                    FailCount = table.Column<int>(type: "int", nullable: false),
                    SkipCount = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    SuccesAnswer = table.Column<int>(type: "int", nullable: false),
                    WrongAnswer = table.Column<int>(type: "int", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(2)", nullable: false, defaultValue: "az")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Languages_LanguageCode",
                        column: x => x.LanguageCode,
                        principalTable: "Languages",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_LanguageCode",
                table: "Game",
                column: "LanguageCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}

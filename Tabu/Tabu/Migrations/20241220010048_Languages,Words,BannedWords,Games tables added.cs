using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabu.Migrations
{
    /// <inheritdoc />
    public partial class LanguagesWordsBannedWordsGamestablesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Languages_LanguageCode",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Game_LanguageCode",
                table: "Games",
                newName: "IX_Games_LanguageCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Languages_LanguageCode",
                        column: x => x.LanguageCode,
                        principalTable: "Languages",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannedWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannedWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannedWords_WordId",
                table: "BannedWords",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LanguageCode",
                table: "Words",
                column: "LanguageCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Languages_LanguageCode",
                table: "Games",
                column: "LanguageCode",
                principalTable: "Languages",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Languages_LanguageCode",
                table: "Games");

            migrationBuilder.DropTable(
                name: "BannedWords");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Languages_Name",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameIndex(
                name: "IX_Games_LanguageCode",
                table: "Game",
                newName: "IX_Game_LanguageCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Languages_LanguageCode",
                table: "Game",
                column: "LanguageCode",
                principalTable: "Languages",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

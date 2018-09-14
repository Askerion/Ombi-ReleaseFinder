using Microsoft.EntityFrameworkCore.Migrations;

namespace OmbiReleaseFinder.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomMovie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "date('now')")
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieDbId = table.Column<int>(nullable: false),
                    OriginalTitle = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Overview = table.Column<string>(nullable: true),
                    PosterPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomMovie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FtpRelease",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "date('now')")
                        .Annotation("Sqlite:Autoincrement", true),
                    FtpReleasename = table.Column<string>(unicode: false, nullable: true),
                    FtpReleaseGroup = table.Column<string>(unicode: false, nullable: true),
                    FtpFolder = table.Column<string>(unicode: false, nullable: true),
                    MovieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FtpRelease", x => x.Id);
                    table.ForeignKey(
                        name: "FK__FtpReleas__Movie__5AEE82B9",
                        column: x => x.MovieId,
                        principalTable: "CustomMovie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Releasenames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "date('now')")
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releasenames", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Releasena__Movie__276EDEB3",
                        column: x => x.MovieId,
                        principalTable: "CustomMovie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FtpRelease_MovieId",
                table: "FtpRelease",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Releasenames_MovieId",
                table: "Releasenames",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FtpRelease");

            migrationBuilder.DropTable(
                name: "Releasenames");

            migrationBuilder.DropTable(
                name: "CustomMovie");
        }
    }
}

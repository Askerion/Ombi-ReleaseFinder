using Microsoft.EntityFrameworkCore.Migrations;

namespace OmbiReleaseFinder.Migrations
{
    public partial class RankingtoMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "CustomMovie",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CustomMovie");
        }
    }
}

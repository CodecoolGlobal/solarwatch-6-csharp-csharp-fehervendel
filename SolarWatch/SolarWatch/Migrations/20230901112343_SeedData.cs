using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarWatch.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "SunsetSunrise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SunId",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SunsetSunrise_CityId",
                table: "SunsetSunrise",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SunsetSunrise_Cities_CityId",
                table: "SunsetSunrise",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SunsetSunrise_Cities_CityId",
                table: "SunsetSunrise");

            migrationBuilder.DropIndex(
                name: "IX_SunsetSunrise_CityId",
                table: "SunsetSunrise");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "SunsetSunrise");

            migrationBuilder.DropColumn(
                name: "SunId",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

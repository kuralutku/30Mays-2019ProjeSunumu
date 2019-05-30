using Microsoft.EntityFrameworkCore.Migrations;

namespace Project4.UI.Migrations
{
    public partial class AddColumnIsSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSlider",
                table: "Blogs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSlider",
                table: "Blogs");
        }
    }
}

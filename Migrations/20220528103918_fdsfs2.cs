using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace termProject_201811010.Migrations
{
    public partial class fdsfs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HideProfile",
                table: "UserProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HideProfile",
                table: "UserProfiles");
        }
    }
}

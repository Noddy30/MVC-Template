using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Migrations
{
    public partial class AddedDeletedToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");
        }
    }
}

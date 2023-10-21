using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Migrations
{
    public partial class ChangesToTeeBosCoursesScorecard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreCards_GolfCourses_GolfCourseId",
                table: "ScoreCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TeeBoxes_GolfCourses_GolfCourseId",
                table: "TeeBoxes");

            migrationBuilder.DropIndex(
                name: "IX_TeeBoxes_GolfCourseId",
                table: "TeeBoxes");

            migrationBuilder.DropIndex(
                name: "IX_ScoreCards_GolfCourseId",
                table: "ScoreCards");

            migrationBuilder.DropColumn(
                name: "GolfCourseId",
                table: "TeeBoxes");

            migrationBuilder.DropColumn(
                name: "GolfCourseId",
                table: "ScoreCards");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TeeBoxes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ScoreCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "GolfCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreCards_GolfCourses_Id",
                table: "ScoreCards",
                column: "Id",
                principalTable: "GolfCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeeBoxes_GolfCourses_Id",
                table: "TeeBoxes",
                column: "Id",
                principalTable: "GolfCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreCards_GolfCourses_Id",
                table: "ScoreCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TeeBoxes_GolfCourses_Id",
                table: "TeeBoxes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TeeBoxes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "GolfCourseId",
                table: "TeeBoxes",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ScoreCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "GolfCourseId",
                table: "ScoreCards",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "GolfCourses",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_TeeBoxes_GolfCourseId",
                table: "TeeBoxes",
                column: "GolfCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreCards_GolfCourseId",
                table: "ScoreCards",
                column: "GolfCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreCards_GolfCourses_GolfCourseId",
                table: "ScoreCards",
                column: "GolfCourseId",
                principalTable: "GolfCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeeBoxes_GolfCourses_GolfCourseId",
                table: "TeeBoxes",
                column: "GolfCourseId",
                principalTable: "GolfCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

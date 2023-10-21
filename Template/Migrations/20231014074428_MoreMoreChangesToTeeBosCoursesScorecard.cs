using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Migrations
{
    public partial class MoreMoreChangesToTeeBosCoursesScorecard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreCards_GolfCourses_Id",
                table: "ScoreCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TeeBoxes_GolfCourses_Id",
                table: "TeeBoxes");

            migrationBuilder.DropColumn(
                name: "Handicap",
                table: "TeeBoxes");

            migrationBuilder.DropColumn(
                name: "Slope",
                table: "TeeBoxes");

            migrationBuilder.DropColumn(
                name: "Tees",
                table: "ScoreCards");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "GolfCourses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GolfCourses");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "GolfCourses");

            migrationBuilder.RenameColumn(
                name: "Tee",
                table: "TeeBoxes",
                newName: "Color");

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
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Yards",
                table: "TeeBoxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TeesId",
                table: "ScoreCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ScoreCards_TeesId",
                table: "ScoreCards",
                column: "TeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreCards_GolfCourses_GolfCourseId",
                table: "ScoreCards",
                column: "GolfCourseId",
                principalTable: "GolfCourses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreCards_TeeBoxes_TeesId",
                table: "ScoreCards",
                column: "TeesId",
                principalTable: "TeeBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeeBoxes_GolfCourses_GolfCourseId",
                table: "TeeBoxes",
                column: "GolfCourseId",
                principalTable: "GolfCourses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreCards_GolfCourses_GolfCourseId",
                table: "ScoreCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoreCards_TeeBoxes_TeesId",
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

            migrationBuilder.DropIndex(
                name: "IX_ScoreCards_TeesId",
                table: "ScoreCards");

            migrationBuilder.DropColumn(
                name: "GolfCourseId",
                table: "TeeBoxes");

            migrationBuilder.DropColumn(
                name: "Yards",
                table: "TeeBoxes");

            migrationBuilder.DropColumn(
                name: "GolfCourseId",
                table: "ScoreCards");

            migrationBuilder.DropColumn(
                name: "TeesId",
                table: "ScoreCards");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "TeeBoxes",
                newName: "Tee");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TeeBoxes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<double>(
                name: "Handicap",
                table: "TeeBoxes",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Slope",
                table: "TeeBoxes",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ScoreCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Tees",
                table: "ScoreCards",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "GolfCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "GolfCourses",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GolfCourses",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "GolfCourses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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
    }
}

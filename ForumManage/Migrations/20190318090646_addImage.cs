using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumManage.Migrations
{
    public partial class addImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Engineers");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Engineers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Engineers");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Engineers",
                nullable: true);
        }
    }
}

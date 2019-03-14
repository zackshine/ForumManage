using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumManage.Migrations
{
    public partial class addNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Engineers_EngineerId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Forums_ForumId",
                table: "Cases");

            migrationBuilder.AlterColumn<long>(
                name: "ForumId",
                table: "Cases",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "EngineerId",
                table: "Cases",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Engineers_EngineerId",
                table: "Cases",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Forums_ForumId",
                table: "Cases",
                column: "ForumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Engineers_EngineerId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Forums_ForumId",
                table: "Cases");

            migrationBuilder.AlterColumn<long>(
                name: "ForumId",
                table: "Cases",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EngineerId",
                table: "Cases",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Engineers_EngineerId",
                table: "Cases",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Forums_ForumId",
                table: "Cases",
                column: "ForumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

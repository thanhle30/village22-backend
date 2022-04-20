using Microsoft.EntityFrameworkCore.Migrations;

namespace Village22.Migrations
{
    public partial class FixTaMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaMatches_AspNetUsers_TaId1",
                table: "TaMatches");

            migrationBuilder.DropIndex(
                name: "IX_TaMatches_TaId1",
                table: "TaMatches");

            migrationBuilder.DropColumn(
                name: "TaId1",
                table: "TaMatches");

            migrationBuilder.AlterColumn<string>(
                name: "TaId",
                table: "TaMatches",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TaMatches_TaId",
                table: "TaMatches",
                column: "TaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaMatches_AspNetUsers_TaId",
                table: "TaMatches",
                column: "TaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaMatches_AspNetUsers_TaId",
                table: "TaMatches");

            migrationBuilder.DropIndex(
                name: "IX_TaMatches_TaId",
                table: "TaMatches");

            migrationBuilder.AlterColumn<int>(
                name: "TaId",
                table: "TaMatches",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaId1",
                table: "TaMatches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaMatches_TaId1",
                table: "TaMatches",
                column: "TaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TaMatches_AspNetUsers_TaId1",
                table: "TaMatches",
                column: "TaId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

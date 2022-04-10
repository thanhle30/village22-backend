using Microsoft.EntityFrameworkCore.Migrations;

namespace Village22.Migrations
{
    public partial class FixTaAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaContracts_Courses_CourseId",
                table: "TaContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_TaRequests_Courses_CourseId",
                table: "TaRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachingAssignments_AspNetUsers_TeacherId1",
                table: "TeachingAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssignments_TeacherId1",
                table: "TeachingAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TaRequests_CourseId",
                table: "TaRequests");

            migrationBuilder.DropIndex(
                name: "IX_TaContracts_CourseId",
                table: "TaContracts");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "TeachingAssignments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "TaRequests");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "TaContracts");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeachingAssignments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_TeacherId",
                table: "TeachingAssignments",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingAssignments_AspNetUsers_TeacherId",
                table: "TeachingAssignments",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingAssignments_AspNetUsers_TeacherId",
                table: "TeachingAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssignments_TeacherId",
                table: "TeachingAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "TeachingAssignments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "TeachingAssignments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "TaRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "TaContracts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_TeacherId1",
                table: "TeachingAssignments",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_TaRequests_CourseId",
                table: "TaRequests",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TaContracts_CourseId",
                table: "TaContracts",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaContracts_Courses_CourseId",
                table: "TaContracts",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaRequests_Courses_CourseId",
                table: "TaRequests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingAssignments_AspNetUsers_TeacherId1",
                table: "TeachingAssignments",
                column: "TeacherId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

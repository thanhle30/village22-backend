using Microsoft.EntityFrameworkCore.Migrations;

namespace Village22.Migrations
{
    public partial class AddTeachingAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_TeacherId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_TaContracts_Courses_CourseId",
                table: "TaContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_TaRequests_Courses_CourseId",
                table: "TaRequests");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TaRequests",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TeachingAssignmentId",
                table: "TaRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TaContracts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TeachingAssignmentId",
                table: "TaContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeachingAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    TeacherId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_AspNetUsers_TeacherId1",
                        column: x => x.TeacherId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaRequests_TeachingAssignmentId",
                table: "TaRequests",
                column: "TeachingAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaContracts_TeachingAssignmentId",
                table: "TaContracts",
                column: "TeachingAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_CourseId",
                table: "TeachingAssignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_TeacherId1",
                table: "TeachingAssignments",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaContracts_Courses_CourseId",
                table: "TaContracts",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaContracts_TeachingAssignments_TeachingAssignmentId",
                table: "TaContracts",
                column: "TeachingAssignmentId",
                principalTable: "TeachingAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaRequests_Courses_CourseId",
                table: "TaRequests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaRequests_TeachingAssignments_TeachingAssignmentId",
                table: "TaRequests",
                column: "TeachingAssignmentId",
                principalTable: "TeachingAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_TaContracts_Courses_CourseId",
                table: "TaContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_TaContracts_TeachingAssignments_TeachingAssignmentId",
                table: "TaContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_TaRequests_Courses_CourseId",
                table: "TaRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TaRequests_TeachingAssignments_TeachingAssignmentId",
                table: "TaRequests");

            migrationBuilder.DropTable(
                name: "TeachingAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TaRequests_TeachingAssignmentId",
                table: "TaRequests");

            migrationBuilder.DropIndex(
                name: "IX_TaContracts_TeachingAssignmentId",
                table: "TaContracts");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeachingAssignmentId",
                table: "TaRequests");

            migrationBuilder.DropColumn(
                name: "TeachingAssignmentId",
                table: "TaContracts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TaRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TaContracts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId1",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId1",
                table: "Courses",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_TeacherId1",
                table: "Courses",
                column: "TeacherId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaContracts_Courses_CourseId",
                table: "TaContracts",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaRequests_Courses_CourseId",
                table: "TaRequests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

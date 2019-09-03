using Microsoft.EntityFrameworkCore.Migrations;

namespace Studenti.Migrations
{
    public partial class ChangedIdentityUserIdToStringInClassAttends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "ClassAttends",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdentityUserId",
                table: "ClassAttends",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

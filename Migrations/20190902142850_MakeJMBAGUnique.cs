using Microsoft.EntityFrameworkCore.Migrations;

namespace Studenti.Migrations
{
    public partial class MakeJMBAGUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JMBAG",
                table: "AspNetUsers",
                column: "JMBAG");


            //migrationBuilder.DropColumn(
            //    name: "Discriminator",
            //    table: "AspNetUsers");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserTokens",
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "Id",
            //    table: "AspNetUsers",
            //    maxLength: 36,
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserRoles",
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserLogins",
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserClaims",
            //    nullable: false,
            //    oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserTokens",
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "Id",
            //    table: "AspNetUsers",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 36);

            //migrationBuilder.AddColumn<string>(
            //    name: "Discriminator",
            //    table: "AspNetUsers",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserRoles",
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserLogins",
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "AspNetUserClaims",
            //    nullable: false,
            //    oldClrType: typeof(string));

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUsers_JMBAG",
            //    table: "AspNetUsers",
            //    column: "JMBAG");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JMBAG",
                table: "AspNetUsers");
        }
    }
}

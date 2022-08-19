using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Client.Persistence.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "People",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "UK_Identification",
                table: "People",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ClientId",
                table: "Clients",
                column: "ClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_Identification",
                table: "People");

            migrationBuilder.DropIndex(
                name: "UK_ClientId",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

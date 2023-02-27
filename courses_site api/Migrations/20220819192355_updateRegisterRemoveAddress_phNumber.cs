using Microsoft.EntityFrameworkCore.Migrations;

namespace courses_site_api.Migrations
{
    public partial class updateRegisterRemoveAddress_phNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "registrations");

            migrationBuilder.DropColumn(
                name: "phonenumber",
                table: "registrations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "registrations",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phonenumber",
                table: "registrations",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }
    }
}

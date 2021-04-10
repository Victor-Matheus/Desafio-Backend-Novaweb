using Microsoft.EntityFrameworkCore.Migrations;

namespace contacts.Migrations
{
    public partial class contacts_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email_Address",
                table: "contacts",
                newName: "email_address");

            migrationBuilder.RenameColumn(
                name: "Name_LastName",
                table: "contacts",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "Name_FirstName",
                table: "contacts",
                newName: "first_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_address",
                table: "contacts",
                newName: "Email_Address");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "contacts",
                newName: "Name_LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "contacts",
                newName: "Name_FirstName");
        }
    }
}

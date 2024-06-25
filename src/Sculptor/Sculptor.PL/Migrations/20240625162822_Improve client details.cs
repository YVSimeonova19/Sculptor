using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sculptor.PL.Migrations
{
    /// <inheritdoc />
    public partial class Improveclientdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientLastName",
                table: "ClientInfo",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "ClientFirstName",
                table: "ClientInfo",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "ClientEmail",
                table: "ClientInfo",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ClientArea",
                table: "ClientInfo",
                newName: "Area");

            migrationBuilder.RenameColumn(
                name: "ClientAddress",
                table: "ClientInfo",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ClientInfo",
                newName: "ClientLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "ClientInfo",
                newName: "ClientFirstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ClientInfo",
                newName: "ClientEmail");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "ClientInfo",
                newName: "ClientArea");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "ClientInfo",
                newName: "ClientAddress");
        }
    }
}

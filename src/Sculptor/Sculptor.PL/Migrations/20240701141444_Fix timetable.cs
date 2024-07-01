using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sculptor.PL.Migrations
{
    /// <inheritdoc />
    public partial class Fixtimetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Timetables_TimetableId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TimetableId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Timetables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_OrderId",
                table: "Timetables",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_Orders_OrderId",
                table: "Timetables",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_Orders_OrderId",
                table: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Timetables_OrderId",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Timetables");

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TimetableId",
                table: "Orders",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Timetables_TimetableId",
                table: "Orders",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

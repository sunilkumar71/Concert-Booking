using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertBooking.WebHost.Migrations
{
    public partial class UpdateSeatCapacityToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Bookings_BookingId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Concerts_ConcertId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_ConcertId",
                table: "Tickets",
                newName: "IX_Tickets_ConcertId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_BookingId",
                table: "Tickets",
                newName: "IX_Tickets_BookingId");

            migrationBuilder.AlterColumn<int>(
                name: "SeatCapacity",
                table: "Venues",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Bookings_BookingId",
                table: "Tickets",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Concerts_ConcertId",
                table: "Tickets",
                column: "ConcertId",
                principalTable: "Concerts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Bookings_BookingId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Concerts_ConcertId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ConcertId",
                table: "Ticket",
                newName: "IX_Ticket_ConcertId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_BookingId",
                table: "Ticket",
                newName: "IX_Ticket_BookingId");

            migrationBuilder.AlterColumn<string>(
                name: "SeatCapacity",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Bookings_BookingId",
                table: "Ticket",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Concerts_ConcertId",
                table: "Ticket",
                column: "ConcertId",
                principalTable: "Concerts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

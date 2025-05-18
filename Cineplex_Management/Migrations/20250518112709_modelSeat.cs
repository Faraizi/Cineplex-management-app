using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cineplex_Management.Migrations
{
    /// <inheritdoc />
    public partial class modelSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowNumber",
                table: "SeatPlan",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowNumber",
                table: "SeatPlan");
        }
    }
}

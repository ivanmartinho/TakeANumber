using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeANumber.Migrations
{
    /// <inheritdoc />
    public partial class EnabledColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "TicketGroup",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Spot",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Company",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "TicketGroup");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Spot");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Company");
        }
    }
}

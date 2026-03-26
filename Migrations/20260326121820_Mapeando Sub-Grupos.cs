using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeANumber.Migrations
{
    /// <inheritdoc />
    public partial class MapeandoSubGrupos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComapanyChildrenId",
                table: "TicketGroup",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketGroup_ComapanyChildrenId",
                table: "TicketGroup",
                column: "ComapanyChildrenId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCompany_CompanyChildrenId",
                table: "TicketGroup",
                column: "ComapanyChildrenId",
                principalTable: "TicketGroup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCompany_CompanyChildrenId",
                table: "TicketGroup");

            migrationBuilder.DropIndex(
                name: "IX_TicketGroup_ComapanyChildrenId",
                table: "TicketGroup");

            migrationBuilder.DropColumn(
                name: "ComapanyChildrenId",
                table: "TicketGroup");
        }
    }
}

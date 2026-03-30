using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeANumberApi.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDoBancoDeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Acronym = table.Column<string>(type: "NVARCHAR(3)", maxLength: 3, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketGroupId = table.Column<int>(type: "int", nullable: false),
                    SpotId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    TicketType = table.Column<int>(type: "int", nullable: false),
                    Called = table.Column<bool>(type: "bit", nullable: false),
                    Serviced = table.Column<bool>(type: "bit", nullable: false),
                    GenerateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServicedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketNumbers_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketNumbers_Spot_SpotId",
                        column: x => x.SpotId,
                        principalTable: "Spot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketNumbers_TicketGroup_TicketGroupId",
                        column: x => x.TicketGroupId,
                        principalTable: "TicketGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketNumbers_CompanyId",
                table: "TicketNumbers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNumbers_SpotId",
                table: "TicketNumbers",
                column: "SpotId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketNumbers_TicketGroupId",
                table: "TicketNumbers",
                column: "TicketGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketNumbers");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Spot");

            migrationBuilder.DropTable(
                name: "TicketGroup");
        }
    }
}

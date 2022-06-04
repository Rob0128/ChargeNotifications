using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChargeNotifications.Data.Migrations
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostPence = table.Column<int>(type: "int", nullable: false),
                    CostTotal = table.Column<int>(type: "int", nullable: false),
                    ChargeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charge", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charge");
        }
    }
}

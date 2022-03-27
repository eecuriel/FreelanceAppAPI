using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceAppAPI.Migrations
{
    public partial class DocumentEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customes",
                columns: table => new
                {
                    CutomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CutomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customes", x => x.CutomerId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetails",
                columns: table => new
                {
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeIn = table.Column<int>(type: "int", nullable: false),
                    TimeOut = table.Column<int>(type: "int", nullable: false),
                    OffTimeStart = table.Column<int>(type: "int", nullable: false),
                    OffTimeEnd = table.Column<int>(type: "int", nullable: false),
                    TotalHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaskComment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetails", x => x.RowId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentHeaders",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CutomerId = table.Column<int>(type: "int", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeekHours = table.Column<int>(type: "int", nullable: false),
                    TotalHoursWorked = table.Column<int>(type: "int", nullable: false),
                    RegularHours = table.Column<int>(type: "int", nullable: false),
                    OvertimeHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentHeaders", x => x.DocId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customes");

            migrationBuilder.DropTable(
                name: "DocumentDetails");

            migrationBuilder.DropTable(
                name: "DocumentHeaders");
        }
    }
}

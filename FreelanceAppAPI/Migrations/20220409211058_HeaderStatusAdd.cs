using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceAppAPI.Migrations
{
    public partial class HeaderStatusAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocState",
                table: "DocumentHeaders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocState",
                table: "DocumentHeaders");
        }
    }
}

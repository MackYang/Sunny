using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunny.Repository.Migrations
{
    public partial class x1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "requestType",
                table: "IdTest",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requestType",
                table: "IdTest");
        }
    }
}

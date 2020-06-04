using Microsoft.EntityFrameworkCore.Migrations;

namespace PostgreWebApp2._2.Migrations
{
    public partial class delUseradde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "e",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "e",
                table: "User",
                type: "text",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PostgreWebApp2._2.Migrations
{
    public partial class upUseraddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "d",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "d",
                table: "User");
        }
    }
}

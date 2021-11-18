using Microsoft.EntityFrameworkCore.Migrations;

namespace USAElections.Migrations
{
    public partial class updateModelsInDatabaseee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number",
                table: "Vote");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Ovning11Garage2._0.Migrations
{
    public partial class WheelProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumnOfWheels",
                table: "ParkedVehicle",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumnOfWheels",
                table: "ParkedVehicle");
        }
    }
}

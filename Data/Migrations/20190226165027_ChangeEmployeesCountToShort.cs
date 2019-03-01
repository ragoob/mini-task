using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangeEmployeesCountToShort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "EmployeesCount",
                schema: "dbo",
                table: "Organizations",
                nullable: false,
                oldClrType: typeof(byte));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "EmployeesCount",
                schema: "dbo",
                table: "Organizations",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Add_Organization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete Projects");
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                schema: "dbo",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeesCount = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                schema: "dbo",
                table: "Projects",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                schema: "dbo",
                table: "Projects",
                column: "OrganizationId",
                principalSchema: "dbo",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OrganizationId",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "dbo",
                table: "Projects");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace P01.Work.Data.Migrations
{
    public partial class CreateWorkDBDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Salary",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}

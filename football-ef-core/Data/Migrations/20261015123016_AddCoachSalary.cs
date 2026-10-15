using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace football_ef_core.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Coaches",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 1500m);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Coach_Salary_NonNegative",
                table: "Coaches",
                sql: "`Salary` >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Coach_Salary_NonNegative",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Coaches");
        }
    }
}

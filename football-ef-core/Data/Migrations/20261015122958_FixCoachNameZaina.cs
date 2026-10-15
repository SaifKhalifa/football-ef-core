using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace football_ef_core.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCoachNameZaina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // UPDATE only: the table and the record are kept
            migrationBuilder.Sql("UPDATE `Coaches` SET `Name` = 'Zain' WHERE `Name` = 'Zaina';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE `Coaches` SET `Name` = 'Zaina' WHERE `Name` = 'Zain';");
        }
    }
}

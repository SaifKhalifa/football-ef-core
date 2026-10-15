using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace football_ef_core.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelRefactorToFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_TrainingSession_TrainingSessionId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSession_Coach_CoachId",
                table: "TrainingSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingSession",
                table: "TrainingSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_TrainingSessionId",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coach",
                table: "Coach");

            migrationBuilder.DropColumn(
                name: "TrainingSessionId",
                table: "Player");

            migrationBuilder.RenameTable(
                name: "TrainingSession",
                newName: "TrainingSessions");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Coach",
                newName: "Coaches");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSession_Title",
                table: "TrainingSessions",
                newName: "IX_TrainingSessions_Title");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSession_CoachId",
                table: "TrainingSessions",
                newName: "IX_TrainingSessions_CoachId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TrainingSessions",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "TrainingSessions",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(CURRENT_DATE)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte>(
                name: "Age",
                table: "Players",
                type: "tinyint unsigned",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingSessions",
                table: "TrainingSessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coaches",
                table: "Coaches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PlayerSessions",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    TrainingSessionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSessions", x => new { x.PlayersId, x.TrainingSessionsId });
                    table.ForeignKey(
                        name: "FK_PlayerSessions_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSessions_TrainingSessions_TrainingSessionsId",
                        column: x => x.TrainingSessionsId,
                        principalTable: "TrainingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Player_Age_Range",
                table: "Players",
                sql: "`Age` BETWEEN 1 AND 50");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSessions_TrainingSessionsId",
                table: "PlayerSessions",
                column: "TrainingSessionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Coaches_CoachId",
                table: "TrainingSessions",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Coaches_CoachId",
                table: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "PlayerSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingSessions",
                table: "TrainingSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Player_Age_Range",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coaches",
                table: "Coaches");

            migrationBuilder.RenameTable(
                name: "TrainingSessions",
                newName: "TrainingSession");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "Coaches",
                newName: "Coach");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSessions_Title",
                table: "TrainingSession",
                newName: "IX_TrainingSession_Title");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingSessions_CoachId",
                table: "TrainingSession",
                newName: "IX_TrainingSession_CoachId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TrainingSession",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "TrainingSession",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(CURRENT_DATE)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Player",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<short>(
                name: "Age",
                table: "Player",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned");

            migrationBuilder.AddColumn<int>(
                name: "TrainingSessionId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coach",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingSession",
                table: "TrainingSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coach",
                table: "Coach",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TrainingSessionId",
                table: "Player",
                column: "TrainingSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_TrainingSession_TrainingSessionId",
                table: "Player",
                column: "TrainingSessionId",
                principalTable: "TrainingSession",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSession_Coach_CoachId",
                table: "TrainingSession",
                column: "CoachId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

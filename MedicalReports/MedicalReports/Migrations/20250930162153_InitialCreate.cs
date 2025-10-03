using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalReports.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guidelines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CholesterolTotalOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolTotalNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolTotalSeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolHdlOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolHdlNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolHdlSeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolLdlOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolLdlNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolLdlSeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodSugarOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodSugarNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodSugarSeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressureOptimalSystolic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressureOptimalDiastolic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressureNeedsAttentionSystolic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressureNeedsAttentionDiastolic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressureSeriousIssueSystolic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodPressureSeriousIssueDiastolic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseWeeklyMinutesOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseWeeklyMinutesNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseWeeklyMinutesSeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SleepQualityOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SleepQualityNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SleepQualitySeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StressLevelsOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StressLevelsNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StressLevelsSeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietQualityOptimal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietQualityNeedsAttention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietQualitySeriousIssue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guidelines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalData_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bloodwork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CholesterolTotal = table.Column<int>(type: "int", nullable: false),
                    CholesterolHdl = table.Column<int>(type: "int", nullable: false),
                    CholesterolLdl = table.Column<int>(type: "int", nullable: false),
                    BloodSugar = table.Column<int>(type: "int", nullable: false),
                    BloodPressureSystolic = table.Column<int>(type: "int", nullable: false),
                    BloodPressureDiastolic = table.Column<int>(type: "int", nullable: false),
                    MedicalDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloodwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bloodwork_MedicalData_MedicalDataId",
                        column: x => x.MedicalDataId,
                        principalTable: "MedicalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseWeeklyMinutes = table.Column<int>(type: "int", nullable: false),
                    SleepQuality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StressLevels = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DietQuality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaires_MedicalData_MedicalDataId",
                        column: x => x.MedicalDataId,
                        principalTable: "MedicalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bloodwork_MedicalDataId",
                table: "Bloodwork",
                column: "MedicalDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalData_ClientId",
                table: "MedicalData",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_MedicalDataId",
                table: "Questionnaires",
                column: "MedicalDataId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bloodwork");

            migrationBuilder.DropTable(
                name: "Guidelines");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "MedicalData");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}

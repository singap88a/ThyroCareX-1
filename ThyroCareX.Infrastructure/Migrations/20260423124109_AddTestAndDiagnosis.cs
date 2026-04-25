using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThyroCareX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTestAndDiagnosis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisResults_Patients_PatientId",
                table: "DiagnosisResults");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "FTI",
                table: "Tests",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NodulePresent",
                table: "Tests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "T3",
                table: "Tests",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "T4U",
                table: "Tests",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TSH",
                table: "Tests",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TT4",
                table: "Tests",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "DiagnosisResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ClassificationLabel",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicalRecommendation",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Confidence",
                table: "DiagnosisResults",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DiagnosisResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FunctionalStatus",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaskImageUrl",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextStep",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverlayImageUrl",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RawResponse",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskLevel",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoiImageUrl",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "DiagnosisResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TiradsStage",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_DoctorId",
                table: "Tests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisResults_TestId",
                table: "DiagnosisResults",
                column: "TestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisResults_Patients_PatientId",
                table: "DiagnosisResults",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisResults_Tests_TestId",
                table: "DiagnosisResults",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Doctors_DoctorId",
                table: "Tests",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisResults_Patients_PatientId",
                table: "DiagnosisResults");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisResults_Tests_TestId",
                table: "DiagnosisResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Doctors_DoctorId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_DoctorId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisResults_TestId",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "FTI",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "NodulePresent",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "T3",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "T4U",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TSH",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TT4",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ClassificationLabel",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "ClinicalRecommendation",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "Confidence",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "FunctionalStatus",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "MaskImageUrl",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "NextStep",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "OverlayImageUrl",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "RawResponse",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "RiskLevel",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "RoiImageUrl",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "TiradsStage",
                table: "DiagnosisResults");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "DiagnosisResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisResults_Patients_PatientId",
                table: "DiagnosisResults",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

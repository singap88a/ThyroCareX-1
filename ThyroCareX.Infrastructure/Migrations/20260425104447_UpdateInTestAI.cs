using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThyroCareX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInTestAI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FnacImagePath",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnThyroxine",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QueryHyperthyroid",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThyroidSurgery",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BethesdaCategory",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BethesdaLabel",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FnacRecommendation",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MalignancyRisk",
                table: "DiagnosisResults",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FnacImagePath",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "OnThyroxine",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "QueryHyperthyroid",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ThyroidSurgery",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "BethesdaCategory",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "BethesdaLabel",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "FnacRecommendation",
                table: "DiagnosisResults");

            migrationBuilder.DropColumn(
                name: "MalignancyRisk",
                table: "DiagnosisResults");
        }
    }
}

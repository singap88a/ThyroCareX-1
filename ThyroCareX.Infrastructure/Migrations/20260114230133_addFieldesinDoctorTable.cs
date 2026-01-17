using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThyroCareX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addFieldesinDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalLicenseNumber",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "MedicalLicenseNumber",
                table: "Doctors");
        }
    }
}

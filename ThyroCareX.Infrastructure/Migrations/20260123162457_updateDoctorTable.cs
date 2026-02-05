using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThyroCareX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CurrentPeriodEnd",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId",
                table: "SubscriptionPlans");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateofBirth",
                table: "Doctors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateofBirth",
                table: "Doctors");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SubscriptionPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentPeriodEnd",
                table: "SubscriptionPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionId",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

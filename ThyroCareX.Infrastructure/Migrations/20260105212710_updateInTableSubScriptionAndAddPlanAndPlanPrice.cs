using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThyroCareX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateInTableSubScriptionAndAddPlanAndPlanPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_SubscriptionPlans_SubscriptionPlanID",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SubscriptionPlanID",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DurationInMonths",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PlanDescription",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PlanName",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanID",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "BillingPeriod",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<string>(
                name: "StripeInvoiceId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillingPeriod = table.Column<int>(type: "int", nullable: false),
                    StripePriceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanPrices_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_DoctorId",
                table: "SubscriptionPlans",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_PlanId",
                table: "SubscriptionPlans",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanPrices_PlanId",
                table: "PlanPrices",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_DoctorId",
                table: "Plans",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Doctors_DoctorId",
                table: "SubscriptionPlans",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Plans_PlanId",
                table: "SubscriptionPlans",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Doctors_DoctorId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Plans_PlanId",
                table: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "PlanPrices");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_DoctorId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_PlanId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "BillingPeriod",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CurrentPeriodEnd",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "StripeInvoiceId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "DurationInMonths",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlanDescription",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlanName",
                table: "SubscriptionPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SubscriptionPlans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPlanID",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SubscriptionPlanID",
                table: "Doctors",
                column: "SubscriptionPlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_SubscriptionPlans_SubscriptionPlanID",
                table: "Doctors",
                column: "SubscriptionPlanID",
                principalTable: "SubscriptionPlans",
                principalColumn: "SubscriptionPlanID");
        }
    }
}

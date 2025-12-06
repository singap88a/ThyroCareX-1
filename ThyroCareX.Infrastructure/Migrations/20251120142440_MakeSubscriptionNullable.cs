using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThyroCareX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeSubscriptionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_SubscriptionPlans_SubscriptionPlanID",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionPlanID",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_SubscriptionPlans_SubscriptionPlanID",
                table: "Doctors",
                column: "SubscriptionPlanID",
                principalTable: "SubscriptionPlans",
                principalColumn: "SubscriptionPlanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_SubscriptionPlans_SubscriptionPlanID",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionPlanID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_SubscriptionPlans_SubscriptionPlanID",
                table: "Doctors",
                column: "SubscriptionPlanID",
                principalTable: "SubscriptionPlans",
                principalColumn: "SubscriptionPlanID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThyroCareX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCoulmnInPostLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PostLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId_DoctorId",
                table: "PostLikes",
                columns: new[] { "PostId", "DoctorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostLikes_PostId_DoctorId",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PostLikes");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes",
                column: "PostId");
        }
    }
}

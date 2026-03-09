using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.API.Migrations
{
    /// <inheritdoc />
    public partial class AddResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetOtpExpiry",
                table: "Users",
                newName: "ResetTokenExpiry");

            migrationBuilder.RenameColumn(
                name: "ResetOtp",
                table: "Users",
                newName: "ResetToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetTokenExpiry",
                table: "Users",
                newName: "ResetOtpExpiry");

            migrationBuilder.RenameColumn(
                name: "ResetToken",
                table: "Users",
                newName: "ResetOtp");
        }
    }
}

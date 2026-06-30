using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiWallet_API.Migrations
{
    /// <inheritdoc />
    public partial class addProviderPaymentIdtoTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProviderPaymentId",
                table: "Transactions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderPaymentId",
                table: "Transactions");
        }
    }
}

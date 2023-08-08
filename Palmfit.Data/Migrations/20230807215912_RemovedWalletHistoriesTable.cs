using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedWalletHistoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletHistories_Wallets_WalletId1",
                table: "WalletHistories");

            migrationBuilder.DropIndex(
                name: "IX_WalletHistories_WalletId1",
                table: "WalletHistories");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "WalletId1",
                table: "WalletHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "WalletId",
                table: "WalletHistories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_WalletId",
                table: "WalletHistories",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletHistories_Wallets_WalletId",
                table: "WalletHistories",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletHistories_Wallets_WalletId",
                table: "WalletHistories");

            migrationBuilder.DropIndex(
                name: "IX_WalletHistories_WalletId",
                table: "WalletHistories");

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "WalletId",
                table: "WalletHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId1",
                table: "WalletHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_WalletId1",
                table: "WalletHistories",
                column: "WalletId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletHistories_Wallets_WalletId1",
                table: "WalletHistories",
                column: "WalletId1",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

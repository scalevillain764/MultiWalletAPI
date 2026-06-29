using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiWallet_API.Migrations
{
    /// <inheritdoc />
    public partial class PostgreInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "_Currency",
                table: "Wallets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wallets",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wallets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Wallets",
                type: "timestamp with time zone USING \"DeletedAt\"::timestamp with time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wallets",
                type: "timestamp with time zone USING \"CreatedAt\"::timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Wallets",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Wallets",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiresAt",
                table: "Users",
                type: "timestamp with time zone USING \"RefreshTokenExpiresAt\"::timestamp with time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "timestamp with time zone USING \"DeletedAt\"::timestamp with time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "ToWalletId",
                table: "Transfers",
                type: "character(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "SourceUserId",
                table: "Transfers",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<int>(
                name: "SourceCurrency",
                table: "Transfers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "SourceAmount",
                table: "Transfers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FromWalletId",
                table: "Transfers",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeRate",
                table: "Transfers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationCurrency",
                table: "Transfers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "DestinationAmount",
                table: "Transfers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transfers",
                type: "timestamp with time zone USING \"CreatedAt\"::timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Transfers",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "WalletId",
                table: "Transactions",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Transactions",
                type: "timestamp with time zone USING \"CreatedAt\"::timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Transactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Transactions",
                type: "character(26)",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 26);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "_Currency",
                table: "Wallets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wallets",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wallets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAt",
                table: "Wallets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone USING \"DeletedAt\"::timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Wallets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone USING \"CreatedAt\"::timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Balance",
                table: "Wallets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Wallets",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenExpiresAt",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone USING \"RefreshTokenExpiresAt\"::timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAt",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone USING \"DeletedAt\"::timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "ToWalletId",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)");

            migrationBuilder.AlterColumn<string>(
                name: "SourceUserId",
                table: "Transfers",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<int>(
                name: "SourceCurrency",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "SourceAmount",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "FromWalletId",
                table: "Transfers",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "ExchangeRate",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationCurrency",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAmount",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Transfers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone USING \"CreatedAt\"::timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Transfers",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "WalletId",
                table: "Transactions",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone USING \"CreatedAt\"::timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Transactions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Transactions",
                type: "TEXT",
                fixedLength: true,
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(26)",
                oldFixedLength: true,
                oldMaxLength: 26);
        }
    }
}

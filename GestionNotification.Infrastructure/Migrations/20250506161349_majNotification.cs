using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionNotification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class majNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Channel",
                table: "Notifications",
                newName: "Canal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Canal",
                table: "Notifications",
                newName: "Channel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

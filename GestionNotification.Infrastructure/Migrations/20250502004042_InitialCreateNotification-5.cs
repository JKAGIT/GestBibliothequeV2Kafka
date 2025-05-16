using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionNotification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateNotification5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Objet",
                table: "Notifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Objet",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

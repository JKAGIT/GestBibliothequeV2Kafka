using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionNotification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateNotification4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInterne",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterne",
                table: "Notifications");
        }
    }
}

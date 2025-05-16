using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDesLivres.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fusionMicroSce23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstRendu",
                table: "Emprunt",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstRendu",
                table: "Emprunt");
        }
    }
}

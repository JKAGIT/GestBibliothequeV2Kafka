using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDesLivres.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Livre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IDCategorie = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livre", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Livre_Categorie_IDCategorie",
                        column: x => x.IDCategorie,
                        principalTable: "Categorie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorie_Code",
                table: "Categorie",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livre_IDCategorie",
                table: "Livre",
                column: "IDCategorie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livre");

            migrationBuilder.DropTable(
                name: "Categorie");
        }
    }
}

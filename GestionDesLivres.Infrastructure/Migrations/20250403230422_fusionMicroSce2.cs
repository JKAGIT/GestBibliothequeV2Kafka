using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionDesLivres.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fusionMicroSce2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categorie_Code",
                table: "Categorie");

            migrationBuilder.CreateTable(
                name: "Usager",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenoms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Courriel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usager", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Matricule = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenoms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Courriel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetourEstimee = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Annuler = table.Column<bool>(type: "bit", nullable: false),
                    IDUsager = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDLivre = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservation_Livre_IDLivre",
                        column: x => x.IDLivre,
                        principalTable: "Livre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Usager_IDUsager",
                        column: x => x.IDUsager,
                        principalTable: "Usager",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emprunt",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDReservation = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetourPrevue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDUsager = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDLivre = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunt", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Emprunt_Livre_IDLivre",
                        column: x => x.IDLivre,
                        principalTable: "Livre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emprunt_Reservation_IDReservation",
                        column: x => x.IDReservation,
                        principalTable: "Reservation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emprunt_Usager_IDUsager",
                        column: x => x.IDUsager,
                        principalTable: "Usager",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retour",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDEmprunt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateRetour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retour", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Retour_Emprunt_IDEmprunt",
                        column: x => x.IDEmprunt,
                        principalTable: "Emprunt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprunt_IDLivre",
                table: "Emprunt",
                column: "IDLivre");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunt_IDReservation",
                table: "Emprunt",
                column: "IDReservation",
                unique: true,
                filter: "[IDReservation] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunt_IDUsager",
                table: "Emprunt",
                column: "IDUsager");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IDLivre",
                table: "Reservation",
                column: "IDLivre");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IDUsager",
                table: "Reservation",
                column: "IDUsager");

            migrationBuilder.CreateIndex(
                name: "IX_Retour_IDEmprunt",
                table: "Retour",
                column: "IDEmprunt",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Retour");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Emprunt");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Usager");

            migrationBuilder.CreateIndex(
                name: "IX_Categorie_Code",
                table: "Categorie",
                column: "Code",
                unique: true);
        }
    }
}

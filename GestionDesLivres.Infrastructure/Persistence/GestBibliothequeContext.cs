using GestionDesLivres.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionDesLivres.Infrastructure.Persistence
{
    public class GestBibliothequeContext : DbContext
    {
        public GestBibliothequeContext(DbContextOptions<GestBibliothequeContext> options) : base(options)
        {
        }
 
        public DbSet<Livre> Livre { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Usager> Usager { get; set; }
        public DbSet<Emprunt> Emprunt { get; set; }
        public DbSet<Retour> Retour { get; set; }
        public DbSet<Reservation> Reservation { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Livre>()
            .HasOne(l => l.Categorie)
            .WithMany(c => c.Livres)
                .HasForeignKey(l => l.IDCategorie)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Usager)
                .WithMany(u => u.Emprunts)
                .HasForeignKey(e => e.IDUsager)
                .OnDelete(DeleteBehavior.Cascade);

            //OnDelete(DeleteBehavior.Restrict) : Un livre ne peut pas être supprimé s'il est emprunté.
            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Livre)
                .WithMany(l => l.Emprunts)
                .HasForeignKey(e => e.IDLivre)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Retour>()
               .HasOne(r => r.Emprunt)
               .WithOne(e => e.Retour)
               .HasForeignKey<Retour>(r => r.IDEmprunt)
               .OnDelete(DeleteBehavior.Cascade);


            // Configurer la relation 1:1 entre Emprunts et Reservations
            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Reservation)
                .WithOne(r => r.Emprunt)
                .HasForeignKey<Emprunt>(e => e.IDReservation)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurer la relation entre Livre et Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Livre)
                .WithMany(l => l.Reservations)
                .HasForeignKey(r => r.IDLivre)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurer la relation entre Usager et Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Usager)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.IDUsager)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}



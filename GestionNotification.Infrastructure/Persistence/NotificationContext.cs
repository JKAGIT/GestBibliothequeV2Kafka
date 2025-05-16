using GestionNotification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionNotification.Infrastructure.Persistence
{
    public class NotificationContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; }
       

        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.ID);
                entity.Property(n => n.Message)
                    .IsRequired()
                    .HasMaxLength(500); 
                entity.Property(n => n.Type).IsRequired();
                entity.Property(n => n.Status).IsRequired();
                entity.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(n => n.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }

}



using Microsoft.EntityFrameworkCore;
using EventDrivenExercise.Data.Models.Entities;

namespace EventDrivenExercise.Data.Models
{
    public partial class EventDrivenDbContext : DbContext
    {
        public EventDrivenDbContext()
        {
        }

        public EventDrivenDbContext(DbContextOptions<EventDrivenDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAuditLog> UserAuditLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAuditLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserAuditLog");

                entity.Property(e => e.Event)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

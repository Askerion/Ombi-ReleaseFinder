using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OmbiReleaseFinder.Models
{
    public partial class MovieDatabaseContext : DbContext
    {
        public MovieDatabaseContext()
        {
        }

        public MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomMovie> CustomMovie { get; set; }
        public virtual DbSet<FtpRelease> FtpRelease { get; set; }
        public virtual DbSet<Releasenames> Releasenames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomMovie>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.OriginalTitle).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<FtpRelease>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FtpFolder).IsUnicode(false);

                entity.Property(e => e.FtpReleaseGroup).IsUnicode(false);

                entity.Property(e => e.FtpReleasename).IsUnicode(false);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.FtpRelease)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__FtpReleas__Movie__5AEE82B9");
            });

            modelBuilder.Entity<Releasenames>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Releasenames)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Releasena__Movie__276EDEB3");
            });
        }
    }
}

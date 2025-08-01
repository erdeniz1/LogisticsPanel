using LogisticsPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsPanel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Gonderi> Gonderiler { get; set; }
        public DbSet<Arac> Araclar { get; set; }
        public DbSet<Rota> Rotalar { get; set; }
        public DbSet<Destek> Destekler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gonderi>()
                .HasOne(g => g.GonderenMusteri)
                .WithMany(m => m.Gonderiler)
                .HasForeignKey(g => g.MusteriId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gonderi>()
                .HasOne(g => g.AtananArac)
                .WithMany(a => a.Gonderiler)
                .HasForeignKey(g => g.AtananAracId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Rota>()
                .HasOne(r => r.Arac)
                .WithMany()
                .HasForeignKey(r => r.AracId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Destek>()
                .HasOne(d => d.Musteri)
                .WithMany()
                .HasForeignKey(d => d.MusteriId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}

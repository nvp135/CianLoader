using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CianLib.Model;
using System.IO;

namespace CianLib.EF
{
    public class CianContext : DbContext
    {
        public DbSet<Offer> Offers { get; set; }
        public DbSet<CianObject> CianObjects { get; set; }
        public DbSet<CianObjectPrice> CianObjectPrices { get; set; }

        public CianContext()
        {}

        public CianContext(DbContextOptions<CianContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Offer>()
                .Property(e => e.insertion_date).HasColumnType("date");
            modelBuilder.Entity<CianObjectPrice>()
                .Property(e => e.insert_date).HasColumnType("date");
            modelBuilder.Entity<CianObject>()
                .HasKey(c => new {c.cian_id, c.city });
            modelBuilder.Entity<CianObjectPrice>()
                .HasOne(p => p.cian_object)
                .WithMany(o => o.prices)
                .HasForeignKey(p => new { p.cian_id, p.city });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var settingsPath = $"{Directory.GetCurrentDirectory()}\\settings.json";
                var configuration = new ConfigurationBuilder()
                        .AddJsonFile(settingsPath, optional: true, reloadOnChange: true)
                        .Build();
                var connectionString = configuration.GetConnectionString("Default");
                System.Console.WriteLine(settingsPath);
                System.Console.WriteLine(connectionString);
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure().CommandTimeout(2000));
            }
        }
    }
}

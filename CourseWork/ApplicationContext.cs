using System;
using Microsoft.EntityFrameworkCore;

namespace CourseWork
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealType> DealTypes { get; set; }
        public DbSet<DealPlace> DealPlaces { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database = coursework; Trusted_Connection = True; ");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new DealConfiguration());
            modelBuilder.ApplyConfiguration(new DealTypeConfiguration());
        }
    }
}

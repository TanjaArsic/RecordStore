using Microsoft.EntityFrameworkCore;
using System;

namespace Models
{
    public class VinylContext : DbContext
    {
        
        public DbSet<IzdavackaKuca> IzdavackeKuce { get; set; }
        public DbSet<Izvodjac> Izvodjaci { get; set; }
        public DbSet<Prodavac> Prodavci { get; set; }
        public DbSet<Prodavnica> Prodavnice { get; set; }
        public DbSet<Vinyl> Ploce { get; set; }

        //konstruktor
        public VinylContext(DbContextOptions options) : base(options)
        {

        }
        //mapiranje ako ne zna framework kako to ide
        /*protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            object p = modelBuilder.Entity<Predmet>()
                        .HasMany<Spoj>()
                        .WithOne(p => p.Predmet);

            Action a = new Action();
        }
        */

    }
}
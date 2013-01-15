using System;
using System.Collections.Generic;
using System.Data.Entity;
using PlacowkaZdrowia.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PlacowkaZdrowia.Models
{
    public class PlacowkaZdrowiaContext : DbContext
    {
        public DbSet<Zabieg> Zabiegi { get; set; }
        public DbSet<Dzial> Dzialy { get; set; }
        public DbSet<Rejestracja> Rejestracje { get; set; }
        public DbSet<Lekarz> Lekarze { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set; }
        public DbSet<Osoba> Osoby { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Lekarz>()
                .HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Lekarz);
            modelBuilder.Entity<Zabieg>()
                .HasMany(c => c.Lekarze).WithMany(i => i.Zabiegi)
                .Map(t => t.MapLeftKey("ZabiegID")
                    .MapRightKey("OsobaID")
                    .ToTable("WykonawcaZabiegu"));
            modelBuilder.Entity<Dzial>()
                .HasOptional(x => x.Administrator);
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CuraVet.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }

        public virtual DbSet<Animale> Animale { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Ditta> Ditta { get; set; }
        public virtual DbSet<Prodotto> Prodotto { get; set; }
        public virtual DbSet<Tipologia> Tipologia { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vendita> Vendita { get; set; }
        public virtual DbSet<Visita> Visita { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animale>()
                .HasMany(e => e.Visita)
                .WithRequired(e => e.Animale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Vendita)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ditta>()
                .HasMany(e => e.Prodotto)
                .WithRequired(e => e.Ditta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotto>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotto>()
                .HasMany(e => e.Vendita)
                .WithRequired(e => e.Prodotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipologia>()
                .HasMany(e => e.Animale)
                .WithRequired(e => e.Tipologia)
                .WillCascadeOnDelete(false);
        }
    }
}

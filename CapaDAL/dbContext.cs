using CapaEN;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CapaDAL
{
    public class dbContext : DbContext
    {
        // Constructor con opciones, usando el tipo correcto
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        // DbSets para las tablas
        public DbSet<CategoriaEN> Categorias { get; set; }
        public DbSet<ProductoEN> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductoEN>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductoEN>()
                .HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.IdCategoria);
        }
    }
}

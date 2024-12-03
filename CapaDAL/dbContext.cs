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
        //public DbSet<Producto> Productos { get; set; }
    }
}

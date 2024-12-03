using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class ProductoDAL
    {
        private readonly dbContext _context;

        public ProductoDAL(dbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(ProductoEN producto)
        {
            int result = 0;
            _context.Productos.Add(producto);
            result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(ProductoEN producto)
        {
            int result = 0;

            var productoDB = await _context.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id);
            if (productoDB != null)
            {
                productoDB.Nombre = producto.Nombre;
                productoDB.Precio = producto.Precio;
                productoDB.IdCategoria = producto.IdCategoria;
                _context.Update(productoDB);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> DeleteAsync(ProductoEN producto)
        {
            int result = 0;
            var productoDB = await _context.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id);

            if (productoDB != null)
            {
                _context.Productos.Remove(productoDB);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<ProductoEN> GetByIdAsync(ProductoEN producto)
        {
            var productoDB = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == producto.Id);
            return productoDB;
        }

        public async Task<List<ProductoEN>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        internal IQueryable<ProductoEN> QuerySelect(IQueryable<ProductoEN> query, ProductoEN producto)
        {
            if (producto.Id > 0)
                query = query.Where(p => p.Id == producto.Id);

            if (!string.IsNullOrWhiteSpace(producto.Nombre))
                query = query.Where(p => p.Nombre.Contains(producto.Nombre));

            if (producto.Precio > 0)
                query = query.Where(p => p.Precio == producto.Precio);

            query = query.OrderByDescending(p => p.Id).AsQueryable();

            if (producto.Top_Aux > 0)
                query = query.Take(producto.Top_Aux).AsQueryable();

            return query;
        }

        // Buscar productos según ciertos criterios
        public async Task<List<ProductoEN>> SearchAsync(ProductoEN producto)
        {
            var productos = new List<ProductoEN>();
            var select = _context.Productos.AsQueryable();
            select = QuerySelect(select, producto);
            productos = await select.ToListAsync();
            return productos;
        }
    }
}

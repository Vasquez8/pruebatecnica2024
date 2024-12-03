using CapaEN;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class CategoriaDAL
    {
        private readonly dbContext _context;

        // Inyección de dependencias a través del constructor
        public CategoriaDAL(dbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(CategoriaEN categoria)
        {
            int result = 0;
            _context.Categorias.Add(categoria);
            result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(CategoriaEN categoria)
        {
            int result = 0;

                var categoriaDB = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
                if (categoriaDB != null)
                {
                    categoriaDB.Nombre = categoria.Nombre;
                    _context.Update(categoriaDB);
                    result = await _context.SaveChangesAsync();
                }
            return result;
        }

        public async Task<int> DeleteAsync(CategoriaEN categoria)
        {
            int result = 0;
                var categoriaDB = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);

                if (categoriaDB != null)
                {
                    _context.Categorias.Remove(categoriaDB);
                    result = await _context.SaveChangesAsync();
                }
            return result;
        }

        public async Task<CategoriaEN> GetByIdAsync(CategoriaEN categoria)
        {
            var categoriaDB = new CategoriaEN();
                categoriaDB = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
            return categoriaDB;
        }

        // Devuelve todo lo que contiene la tabla
        public async Task<List<CategoriaEN>> GetAllAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        internal IQueryable<CategoriaEN> QuerySelect(IQueryable<CategoriaEN> query, CategoriaEN categoria)
        {
            if (categoria.Id > 0)
                query = query.Where(c => c.Id == categoria.Id);

            if (!string.IsNullOrWhiteSpace(categoria.Nombre))
                query = query.Where(c => c.Nombre.Contains(categoria.Nombre));

            query = query.OrderByDescending(c => c.Id).AsQueryable();

            if (categoria.Top_Aux > 0)
                query = query.Take(categoria.Top_Aux).AsQueryable();

            return query;
        }

        public async Task<List<CategoriaEN>> SearchAsync(CategoriaEN categoria)
        {
            var categorias = new List<CategoriaEN>();
                var select = _context.Categorias.AsQueryable();
                select = QuerySelect(select, categoria);
                categorias = await select.ToListAsync();
            return categorias;
        }
    }
}

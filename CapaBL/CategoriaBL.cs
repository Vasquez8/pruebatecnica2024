using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class CategoriaBL
    {
        private readonly CategoriaDAL _categoriaDAL;

        public CategoriaBL(CategoriaDAL categoriaDAL)
        {
            _categoriaDAL = categoriaDAL;
        }

        public async Task<int> CreateAsync(CategoriaEN categoria)
        {
            return await _categoriaDAL.CreateAsync(categoria);
        }

        public async Task<int> UpdateAsync(CategoriaEN categoria)
        {
            return await _categoriaDAL.UpdateAsync(categoria);
        }

        public async Task<int> DeleteAsync(CategoriaEN categoria)
        {
            return await _categoriaDAL.DeleteAsync(categoria);
        }

        public async Task<CategoriaEN> GetByIdAsync(int id)
        {
            return await _categoriaDAL.GetByIdAsync(new CategoriaEN { Id = id });
        }

        public async Task<List<CategoriaEN>> GetAllAsync()
        {
            return await _categoriaDAL.GetAllAsync();
        }

        public async Task<List<CategoriaEN>> SearchAsync(CategoriaEN categoria)
        {
            return await _categoriaDAL.SearchAsync(categoria);
        }
    }
}

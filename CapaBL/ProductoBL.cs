using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class ProductoBL
    {
        private readonly ProductoDAL _productoDAL;

        public ProductoBL(ProductoDAL productoDAL)
        {
            _productoDAL = productoDAL;
        }
        public async Task<int> CreateAsync(ProductoEN producto)
        {
            return await _productoDAL.CreateAsync(producto);
        }
        public async Task<int> UpdateAsync(ProductoEN producto)
        {
            return await _productoDAL.UpdateAsync(producto);
        }

        public async Task<int> DeleteAsync(ProductoEN producto)
        {
            return await _productoDAL.DeleteAsync(producto);
        }

        public async Task<ProductoEN> GetByIdAsync(int id)
        {
            return await _productoDAL.GetByIdAsync(new ProductoEN { Id = id });
        }

        public async Task<List<ProductoEN>> GetAllAsync()
        {
            return await _productoDAL.GetAllAsync();
        }

        public async Task<List<ProductoEN>> SearchAsync(ProductoEN categoria)
        {
            return await _productoDAL.SearchAsync(categoria);
        }
    }
}

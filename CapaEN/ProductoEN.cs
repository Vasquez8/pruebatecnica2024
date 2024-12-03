using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class ProductoEN
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }

        public List<CategoriaEN> Categorias { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
        public CategoriaEN Categoria { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class CategoriaEN
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}

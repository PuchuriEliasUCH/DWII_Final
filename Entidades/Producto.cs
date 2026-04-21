using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public int Id_producto { get; set; }
        public int Id_categoria { get; set; }
        public string Nombre { get; set; }
        public string Desc_corta { get; set; }
        public string Desc_completa { get; set; }
        public Decimal Precio { get; set; }
        public Boolean Requiere_preparacion { get; set; }
        public Boolean Estado { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
    }
}

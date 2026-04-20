using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Detalle_pedido
    {
        public int Id_detalle_pedido { get; set; }
        public int Id_pedido { get; set; }
        public int Id_producto { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio_unitario { get; set; }
        public Decimal Subtotal { get; set; }
        public string Observacion { get; set; }
        public string Estado_detalle { get; set; }
        public Boolean Es_adicional { get; set; }
        public DateTime Fecha_registro { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
    }
}

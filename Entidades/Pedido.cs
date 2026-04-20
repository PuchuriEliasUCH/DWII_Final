using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedido
    {
        public int Id_pedido { get; set; }
        public int Id_mesa { get; set; }
        public int Id_mesero { get; set; }
        public DateTime FechaPedido { get; set; }
        public Boolean Estado { get; set; }
        public string Observacion_general { get; set; }
        public Decimal Subtotal { get; set; }
        public Decimal Descuento { get; set; }
        public Decimal Total { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
    }
}

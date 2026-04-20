using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public int Id_usuario { get; set; }
        public int Id_Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contra_hash { get; set; }
        public Boolean Estado { get; set; }
        public DateTime Fecha_registro { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dto.Usuarios
{
    public class UsuarioGuardarDTO
    {
        public int Id_usuario { get; set; }
        public int Id_rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contra_hash { get; set; }

    }
}

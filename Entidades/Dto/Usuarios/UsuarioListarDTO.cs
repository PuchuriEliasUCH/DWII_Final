using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Dto.Usuarios
{
    public class UsuarioListarDTO
    {
        public int Id_usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string NombreRol { get; set; }
    }
}

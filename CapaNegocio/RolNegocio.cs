using CapaDatos;
using CapaDatos.Repositorios;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RolNegocio
    {
        public List<Rol> Listar()
        {
            using (var tx = new Transacciones())
            {
                var repo = new RolRepository(tx);
                return repo.Listar();
            }
        }
    }
}

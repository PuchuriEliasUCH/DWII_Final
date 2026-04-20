using CapaDatos;
using CapaDatos.Repositorios;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Entidades.Dto.Usuarios;

namespace CapaNegocio
{
    public class UsuarioNegocio
    {
        public List<UsuarioListarDTO> ListarDTO() {
            using (var tx = new Transacciones()) 
            {
                var repo = new UsuarioRepository(tx);
                return repo.ListarDTO();
            }
        }

        public Usuario ObtenerPorId(int id)
        {
            using (var tx = new Transacciones())
            {
                var repo = new UsuarioRepository(tx);
                return repo.ObtenerPorId(id);
            }
        }

        public bool Insertar(UsuarioGuardarDTO usuario)
        {
            using (var tx = new Transacciones())
            {
                try
                {

                    usuario.Contra_hash = BCrypt.Net.BCrypt.HashPassword(usuario.Contra_hash);

                    // BCrypt.Net.BCrypt.Verify(ingresada, base_de_datos)

                    var repo = new UsuarioRepository(tx);
                    repo.Insertar(usuario);
                    tx.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    tx.Rollback();
                    return false;
                }
            }
        }

        public bool Actualizar(UsuarioGuardarDTO usuario)
        {
            using (var tx = new Transacciones())
            {
                try
                {
                    if (string.IsNullOrEmpty(usuario.Contra_hash)) 
                    { 
                        usuario.Contra_hash = null;
                    } else { 
                        usuario.Contra_hash = BCrypt.Net.BCrypt.HashPassword(usuario.Contra_hash);
                    }

                    var repo = new UsuarioRepository(tx);
                    repo.Actualizar(usuario);
                    tx.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    tx.Rollback();
                    throw ex;
                    return false;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (var tx = new Transacciones())
            {
                try
                {
                    var repo = new UsuarioRepository(tx);
                    repo.Eliminar(id);
                    tx.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    tx.Rollback();
                    return false;
                }
            }
        }
    }
}

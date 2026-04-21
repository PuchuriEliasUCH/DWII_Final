using CapaDatos;
using CapaDatos.Repositorios;
using Entidades;
using Entidades.Dto.Usuarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UsuarioNegocio
    {
        public List<UsuarioListarDTO> ListarDTO()
        {
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

        public Usuario Autenticar(string correo, string contrasena)
        {
            using (var tx = new Transacciones())
            {
                var repo = new UsuarioRepository(tx);
                var usuario = repo.ObtenerPorCorreo(correo);

                if (usuario == null) return null;
                if (!usuario.Estado) return null;

                bool claveValida = BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contra_hash);

                return claveValida ? usuario : null;
            }
        }

        public bool Insertar(UsuarioGuardarDTO usuario)
        {
            using (var tx = new Transacciones())
            {
                try
                {
                    usuario.Contra_hash = BCrypt.Net.BCrypt.HashPassword(usuario.Contra_hash);

                    var repo = new UsuarioRepository(tx);
                    repo.Insertar(usuario);
                    tx.Commit();
                    return true;
                }
                catch (SqlException)
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
                    }
                    else
                    {
                        usuario.Contra_hash = BCrypt.Net.BCrypt.HashPassword(usuario.Contra_hash);
                    }

                    var repo = new UsuarioRepository(tx);
                    repo.Actualizar(usuario);
                    tx.Commit();
                    return true;
                }
                catch (SqlException)
                {
                    tx.Rollback();
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
                catch (SqlException)
                {
                    tx.Rollback();
                    return false;
                }
            }
        }
    }
}
using CapaDatos.Interfaces;
using Entidades;
using Entidades.Dto.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositorios
{
    public class UsuarioRepository: Conexion
    {
        private readonly Transacciones _transaction;

        public UsuarioRepository(Transacciones transaction) {
            _transaction = transaction;
        }

        public List<UsuarioListarDTO> ListarDTO()
        {
            var lista = new List<UsuarioListarDTO>();

            using (var con = new SqlConnection(_cnx))

            using (var da = new SqlDataAdapter("sp_listar_usuarios_dto", _cnx)) {
                var ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows) {
                    lista.Add(new UsuarioListarDTO { 
                        Id_usuario = (int)row["id_usuario"],
                        NombreCompleto = row["NombreCompleto"].ToString(),
                        Correo = row["correo"].ToString(),
                        NombreRol = row["nombre"].ToString(),
                    });
                }
            }

            return lista;
        }

        public Usuario ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(_cnx))
            using (var da = new SqlDataAdapter("select * from usuario where id_usuario = @id", con)) 
            {
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                var ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0) return null;

                var row = ds.Tables[0].Rows[0];
                return new Usuario
                {
                    Id_usuario = (int)row["id_usuario"],
                    Id_Rol = (int)row["id_rol"],
                    Nombre = row["nombre"].ToString(),
                    Apellido = row["apellido"].ToString(),
                    Correo = row["correo"].ToString(),
                    Contra_hash = row["contra_hash"].ToString(),
                    Estado = (bool)row["estado"],
                    Fecha_registro = (DateTime)row["fecha_registro"]
                };
            }
        }

        public bool Insertar(UsuarioGuardarDTO usuario)
        {
            using (var cmd = _transaction.CrearTransaccion()) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_crear_usuario";

                cmd.Parameters.AddWithValue("@id_rol", usuario.Id_rol);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@contra_hash", usuario.Contra_hash);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public bool Actualizar(UsuarioGuardarDTO usuario)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_actualizar_usuario";

                cmd.Parameters.AddWithValue("@id", usuario.Id_usuario);
                cmd.Parameters.AddWithValue("@id_rol", usuario.Id_rol);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@contra_hash", usuario.Contra_hash);

                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool Eliminar(int id)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_eliminar_usuario";

                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

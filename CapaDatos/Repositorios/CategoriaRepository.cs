using CapaDatos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositorios
{
    public class CategoriaRepository : Conexion, ICrudGeneral<Categoria_productos>
    {

        private readonly Transacciones _transaction;

        public CategoriaRepository(Transacciones transaction)
        {
            _transaction = transaction;
        }

        public List<Categoria_productos> Listar()
        {
            var lista = new List<Categoria_productos>();

            using (var con = new SqlConnection(_cnx))
            using (var da = new SqlDataAdapter("select * from categoria_producto", con))
            {

                var ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(new Categoria_productos
                    {
                        Id_Categoria = (int)row["id_categoria"],
                        Nombre = row["nombre"].ToString(),
                        Descripcion = row["descripcion"] == DBNull.Value ? string.Empty : row["descripcion"].ToString(),
                        Estado = (bool)row["estado"]
                    });
                }
            }

            return lista;
        }

        public bool Insertar(Categoria_productos categoria)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_crear_categoria_producto";

                cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@descripcion",
                    string.IsNullOrWhiteSpace(categoria.Descripcion) ? (object)DBNull.Value : categoria.Descripcion);
                cmd.Parameters.AddWithValue("@estado", categoria.Estado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Categoria_productos categoria)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_actualizar_categoria_producto";

                cmd.Parameters.AddWithValue("@id", categoria.Id_Categoria);
                cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@descripcion",
                    string.IsNullOrWhiteSpace(categoria.Descripcion) ? (object)DBNull.Value : categoria.Descripcion);
                cmd.Parameters.AddWithValue("@estado", categoria.Estado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_eliminar_categoria_producto";

                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

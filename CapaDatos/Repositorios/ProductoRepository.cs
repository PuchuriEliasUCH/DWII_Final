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
    public class ProductoRepository: Conexion, ICrudGeneral<Producto>
    {

        private readonly Transacciones _transaction;

        public ProductoRepository(Transacciones transaction)
        {
            _transaction = transaction;
        }

        public List<Producto> Listar()
        {
            var lista = new List<Producto>();

            using (var con = new SqlConnection(_cnx))
            using (var da = new SqlDataAdapter("select * from producto", con))
            {
                var ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(new Producto
                    {
                        Id_producto = (int)row["id_producto"],
                        Id_categoria = (int)row["id_categoria"],
                        Nombre = row["nombre"].ToString(),
                        Desc_corta = row["desc_corta"].ToString(),
                        Desc_completa = row["desc_completa"].ToString(),
                        Precio = (decimal)row["precio"],
                        Requiere_preparacion = (bool)row["requiere_preparacion"],
                        Estado = (bool)row["estado"],
                        Created_at = (DateTime)row["created_at"]
                    });
                }
            }

            return lista;
        }

        public bool Insertar(Producto entidad)
        {
            using (var cmd = _transaction.CrearTransaccion()) 
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_crear_producto";

                cmd.Parameters.AddWithValue("@id_categoria", entidad.Id_categoria);
                cmd.Parameters.AddWithValue("@nombre", entidad.Nombre);
                cmd.Parameters.AddWithValue("@desc_corta", entidad.Desc_corta);
                cmd.Parameters.AddWithValue("@desc_completa", entidad.Desc_completa);
                cmd.Parameters.AddWithValue("@precio", entidad.Precio);
                cmd.Parameters.AddWithValue("@requiere_preparacion", entidad.Requiere_preparacion);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Producto entidad)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_actualizar_producto";

                cmd.Parameters.AddWithValue("@id_producto", entidad.Id_producto);
                cmd.Parameters.AddWithValue("@id_categoria", entidad.Id_categoria);
                cmd.Parameters.AddWithValue("@nombre", entidad.Nombre);
                cmd.Parameters.AddWithValue("@desc_corta", entidad.Desc_corta);
                cmd.Parameters.AddWithValue("@desc_completa", entidad.Desc_completa);
                cmd.Parameters.AddWithValue("@precio", entidad.Precio);
                cmd.Parameters.AddWithValue("@requiere_preparacion", entidad.Requiere_preparacion);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_eliminar_producto";

                cmd.Parameters.AddWithValue("@id_producto", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}

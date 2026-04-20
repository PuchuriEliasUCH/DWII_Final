using CapaDatos.Interfaces;
using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositorios
{
    public class MesaRepository : Conexion, ICrudGeneral<Mesa>
    {
        private readonly Transacciones _transaction;

        public MesaRepository(Transacciones transaction) { 
            _transaction = transaction;
        }

        public List<Mesa> Listar()
        {
            var lista = new List<Mesa>();

            using (var con = new SqlConnection(_cnx))
            using (var da = new SqlDataAdapter("select * from mesa", con))
            {
                var ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows) {
                    lista.Add(new Mesa
                    {
                        Id_mesa = (int)row["id_mesa"],
                        Numero = (int)row["numero"],
                        Capacidad = (int)row["capacidad"],
                        Estado = row["estado"].ToString()
                    });
                }
                    
            }

            return lista;
        }

        public bool Insertar(Mesa mesa)
        {
            using (var cmd = _transaction.CrearTransaccion()) {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_crear_mesa";

                cmd.Parameters.AddWithValue("@numero", mesa.Numero);
                cmd.Parameters.AddWithValue("@capacidad", mesa.Capacidad);
                cmd.Parameters.AddWithValue("@estado", mesa.Estado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Mesa mesa)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_actualizar_mesa";

                cmd.Parameters.AddWithValue("@id", mesa.Id_mesa);
                cmd.Parameters.AddWithValue("@numero", mesa.Numero);
                cmd.Parameters.AddWithValue("@capacidad", mesa.Capacidad);
                cmd.Parameters.AddWithValue("@estado", mesa.Estado);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var cmd = _transaction.CrearTransaccion())
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_eliminar_mesa";

                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
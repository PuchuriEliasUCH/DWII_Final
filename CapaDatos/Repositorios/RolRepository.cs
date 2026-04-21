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
    public class RolRepository: Conexion
    {
        private readonly Transacciones _transaction;
        public RolRepository(Transacciones transaction) 
        { 
            _transaction = transaction;
        }

        public List<Rol> Listar() {

            var lista = new List<Rol>();

            using (var con = new SqlConnection(_cnx))
            using (var da = new SqlDataAdapter("select * from rol", con)) 
            {
                var ds = new DataSet();
                da.Fill(ds);
                
                foreach (DataRow row in ds.Tables[0].Rows) 
                {
                    lista.Add(new Rol
                    {
                        Id_rol = (int)row["id_rol"],
                        Nombre = row["nombre"].ToString(),
                    });
                }
            }

                return lista;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Unit of Work
// Maneja transacciones
namespace CapaDatos
{
    public class Transacciones : Conexion, IDisposable
    {
        private readonly SqlConnection _conexion;
        private readonly SqlTransaction _transaccion;

        public Transacciones() {
            _conexion = new SqlConnection(_cnx);
            _conexion.Open();
            _transaccion = _conexion.BeginTransaction();
        }

        public SqlCommand CrearTransaccion() { 
            var cmd = _conexion.CreateCommand();
            cmd.Transaction = _transaccion;
            return cmd;
        }

        public void Commit() { 
            _transaccion.Commit();
        }

        public void Rollback() { 
            _transaccion.Rollback();
        }

        public void Dispose()
        {
            _transaccion?.Dispose();
            _conexion?.Dispose();
        }
    }
}

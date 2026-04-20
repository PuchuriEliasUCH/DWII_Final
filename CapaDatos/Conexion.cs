using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public abstract class Conexion
    {
        protected readonly string _cnx;

        protected Conexion() {
            _cnx = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }
    }
}

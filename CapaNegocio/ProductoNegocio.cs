using CapaDatos;
using CapaDatos.Repositorios;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductoNegocio
    {

        public List<Producto> Listar()
        {
            using (var tx = new Transacciones())
            {
                var repo = new ProductoRepository(tx);
                return repo.Listar();
            }
        }

        public bool Insertar(Producto producto)
        {
            using (var tx = new Transacciones())
            {
                try
                {
                    var repo = new ProductoRepository(tx);
                    bool exito = repo.Insertar(producto);
                    if (!exito)
                    {
                        tx.Rollback();
                        return false;
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tx.Rollback();
                    return false;
                }
            }
        }

        public bool Actualizar(Producto producto)
        {
            using (var tx = new Transacciones())
            {
                try
                {
                    var repo = new ProductoRepository(tx);
                    bool exito = repo.Actualizar(producto);

                    if (!exito) { tx.Rollback(); return false; }

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
                    var repo = new ProductoRepository(tx);
                    bool exito = repo.Eliminar(id);

                    if (!exito) { tx.Rollback(); return false; }

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

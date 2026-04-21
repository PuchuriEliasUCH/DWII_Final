using CapaDatos;
using Entidades;
using CapaDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CategoriaProductoNegocio
    {
        public List<Categoria_productos> Listar()
        {
            using (var tx = new Transacciones())
            {
                var repo = new CategoriaRepository(tx);
                return repo.Listar();
            }
        }

        public bool Insertar(Categoria_productos categoria)
        {
            using (var tx = new Transacciones())
            {
                try
                {
                    var repo = new CategoriaRepository(tx);
                    bool exito = repo.Insertar(categoria);

                    if (!exito)
                    {
                        tx.Rollback();
                        return false;
                    }

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

        public bool Actualizar(Categoria_productos categoria)
        {
            using (var tx = new Transacciones())
            {
                try
                {
                    var repo = new CategoriaRepository(tx);
                    bool exito = repo.Actualizar(categoria);

                    if (!exito)
                    {
                        tx.Rollback();
                        return false;
                    }

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
                    var repo = new CategoriaRepository(tx);
                    bool exito = repo.Eliminar(id);

                    if (!exito)
                    {
                        tx.Rollback();
                        return false;
                    }

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

using CapaDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Repositorios;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace CapaNegocio
{
    public class MesaNegocio
    {
        public List<Mesa> Listar()
        {
            using (var tx = new Transacciones())
            {
                var repo = new MesaRepository(tx);
                return repo.Listar();
            }
        }

        public bool Insertar(Mesa mesa) {
            using (var tx = new Transacciones()) {
                try {
                    var repo = new MesaRepository(tx);
                    repo.Insertar(mesa);
                    tx.Commit();
                    return true;
                } catch (SqlException ex) {
                    tx.Rollback();
                    return false;
                }
            }
        }

        public bool Actualizar(Mesa mesa) {
            using (var tx = new Transacciones()) {
                try { 
                    var repo = new MesaRepository(tx);
                    repo.Actualizar(mesa);
                    tx.Commit();
                    return true;
                } catch (SqlException ex) {
                    tx.Rollback();
                    return false;
                }
            }
        }

        public bool Eliminar(int id) {
            using (var tx = new Transacciones()) {
                try {
                    var repo = new MesaRepository(tx);
                    repo.Eliminar(id);
                    tx.Commit();
                    return true;
                } catch (SqlException ex) {
                    tx.Rollback();
                    return false;
                }
            }
        }
    }
}

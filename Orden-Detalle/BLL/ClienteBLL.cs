using Microsoft.EntityFrameworkCore;
using Orden_Detalle.DAL;
using Orden_Detalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Orden_Detalle.BLL
{
    public class ClienteBLL
    {
        public static bool Guardar(Clientes clientes)
        {
            bool paso = false;
            Contexto db = new Contexto();


            try
            {
                if (db.clientes.Add(clientes) != null)
                    paso = db.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;

        }


        public static bool Modificar(Clientes clientes)
        {
            bool paso = false;
            Contexto db = new Contexto();


            try
            {

                db.Entry(clientes).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);

            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }


        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.clientes.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;

                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;

        }


        public static Clientes Buscar(int id)
        {
            Clientes clientes = new Clientes();
            Contexto db = new Contexto();

            try
            {
                clientes = db.clientes.Find(id);
            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                db.Dispose();
            }

            return clientes;

        }

        public static List<Clientes> GetList(Expression<Func<Clientes, bool>> clientes)
        {
            List<Clientes> Lista = new List<Clientes>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.clientes.Where(clientes).ToList();
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;

        }
    }
}

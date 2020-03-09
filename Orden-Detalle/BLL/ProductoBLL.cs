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
    public class ProductoBLL
    {
        public static bool Guardar(Productos productos)
        {
            bool paso = false;
            Contexto db = new Contexto();


            try
            {
                if (db.productos.Add(productos) != null)
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



        public static bool Modificar(Productos productos)
        {
            bool paso = false;
            Contexto db = new Contexto();


            try
            {

                db.Entry(productos).State = EntityState.Modified;
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
                var eliminar = db.productos.Find(id);
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


        public static Productos Buscar(int id)
        {
            Productos productos = new Productos();
            Contexto db = new Contexto();

            try
            {
               productos =  db.productos.Find(id);
            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                db.Dispose();
            }

            return productos;

        }

        public static List<Productos> GetList(Expression<Func<Productos, bool>> productos)
        {
            List<Productos> Lista = new List<Productos>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.productos.Where(productos).ToList();
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

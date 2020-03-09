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
    public class OrdenBLL
    {
        public static bool Guardar(Ordenes ordenes)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.ordenes.Add(ordenes) != null)
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


        public static bool Modificar(Ordenes ordenes)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {

                var Anterior = OrdenBLL.Buscar(ordenes.OrdenId);
                foreach (var item in Anterior.Detalle)
                {
                    if (!ordenes.Detalle.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;

                }

                foreach (var item in ordenes.Detalle)
                {

                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = estado;
                }

                db.Entry(ordenes).State = EntityState.Modified;
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

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.ordenes.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
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

        public static Ordenes Buscar(int id)
        {

            Contexto db = new Contexto();
            Ordenes ordenes = new Ordenes();

            try
            {
                ordenes = db.ordenes.Where(o => o.OrdenId == id).Include(o => o.Detalle).SingleOrDefault();
                //personas.Telefonos.Count();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return ordenes;
        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> persona)
        {
            List<Ordenes> Lista = new List<Ordenes>();
            Contexto db = new Contexto();

            try
            {

                Lista = db.ordenes.Where(persona).ToList();

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

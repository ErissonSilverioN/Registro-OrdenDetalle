using Microsoft.EntityFrameworkCore;
using Orden_Detalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orden_Detalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Productos> productos  { get; set; }
        public DbSet<Ordenes> ordenes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source = OrdenesDetalle2.db");
        }
    }
}

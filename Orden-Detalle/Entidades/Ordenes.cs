using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Orden_Detalle.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public int ClienteId { get; set; }
        public int Producto { get; set; }
        //public int Cantidad { get; set; }


        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("OrdenId")]
        public List<OrdenDetalle> Detalle { get; set; } = new List<OrdenDetalle>();

        public Ordenes()
        {
            OrdenId = 0;
            ClienteId = 0;
            Producto = 0;
            //Cantidad = 0;
            Monto = 0;
            Fecha = DateTime.Now;
        }

        
        

    }
}

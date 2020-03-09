using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Orden_Detalle.Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }

        [ForeignKey("ClienteId")]
        public List<OrdenDetalle> Detalle { get; set; } = new List<OrdenDetalle>();
        public Clientes()
        {
            ClienteId = 0;
            Nombre = string.Empty;
        }
    }
}

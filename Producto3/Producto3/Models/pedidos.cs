using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Producto3.Models
{
    public class pedidos
    {
        public int idPedido { get; set; }
        public int idArt { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal totalGeneral { get; set; }
        public DateTime fechaCompra { get; set; }
        public int idUsuario { get; set; }
    }
}
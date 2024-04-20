using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Producto3.Models
{
    public class ClientePorArticulo
    {
        public string NombreCliente { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; }
    }
}
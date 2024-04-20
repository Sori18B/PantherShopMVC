using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Producto3.Models
{
    public class productos
    {
        public int idArt { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }
    }
}
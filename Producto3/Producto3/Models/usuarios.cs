using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Producto3.Models
{
    public class usuarios
    {
        public int id { get; set; }
        public string nomCompleto { get; set; }

        [Required(ErrorMessage = "El campo correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
        public string correo { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        public string contra { get; set; }

        public string rol { get; set; }
    }
}
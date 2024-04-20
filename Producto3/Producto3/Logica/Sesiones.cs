using Producto3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Producto3.Logica
{
    public class Sesiones
    { 

        private Datos objDatos = new Datos();
        public string RegistraUsuario(usuarios nuevoUsuario)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@nomCompleto", nuevoUsuario.nomCompleto),
                new SqlParameter("@correo", nuevoUsuario.correo),
                new SqlParameter("@contra", nuevoUsuario.contra),
                new SqlParameter("@rol", nuevoUsuario.rol)
            };

            int nc = objDatos.EjecutaSql("insert into usuarios (nomCompleto, correo, contra, rol) values (@nomCompleto, @correo, @contra, @rol)", pars);
            if (nc == 1)
                return "El usuario ha sido registrado";
            else
                return "No se registró el usuario";
        }

        public string IniciarSesion(string correo, string contra)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@correo", correo),
        new SqlParameter("@contra", contra)
            };

            int nc = objDatos.EjecutaSql("SELECT COUNT(*) FROM usuarios WHERE correo = @correo AND contra = @contra", pars);

            if (nc == 1)
                return "Inicio de sesión exitoso";
            else
                return "Correo o contraseña incorrectos";
        }

        public int ObtenerIdUsuario(string correo, string contra)
        {
            SqlParameter[] pars = new SqlParameter[]
            {
        new SqlParameter("@correo", correo),
        new SqlParameter("@contra", contra)
            };

            SqlDataReader reader = objDatos.ObtenerDatos2arg("SELECT id FROM usuarios WHERE correo = @correo AND contra = @contra", pars);

            int idUsuario = 0;
            if (reader.Read())
            {
                idUsuario = reader.GetInt32(reader.GetOrdinal("id"));
            }
            reader.Close();

            return idUsuario;
        }




    }
}
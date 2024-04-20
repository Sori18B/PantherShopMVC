using Producto3.Logica;
using Producto3.Logica.Filtros;
using Producto3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace Producto3.Controllers
{
    [SepararSesionesFilter]
    public class HomeController : Controller
    {
        Datos objDatos = new Datos();
        funcComprador comprador = new funcComprador();
        Sesiones sesiones = new Sesiones();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registro()
        {
                return View();
            
        }
        [HttpPost]
        public ActionResult Registro(usuarios nuevoUsuario)
        {
            string mensaje = new Sesiones().RegistraUsuario(nuevoUsuario);

            if (mensaje == "El usuario ha sido registrado")
            {
                return RedirectToAction("RegistroExitoso");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View();
            }
        }

        public ActionResult RegistroExitoso()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(usuarios modelo)
        {
            if (ModelState.IsValid)
            {
                string mensaje = sesiones.IniciarSesion(modelo.correo, modelo.contra);

                if (mensaje == "Inicio de sesión exitoso")
                {
                    int idUsuario = sesiones.ObtenerIdUsuario(modelo.correo, modelo.contra);
                    Session["CompradorID"] = idUsuario;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = mensaje;
                    return View();
                }
            }

            return View(modelo);
        }



        public ActionResult CerrarSesion()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }


        public ActionResult CatalogoArticulos()
        {
            List<productos> listaProductos = comprador.ObtenerProductos();
            return View(listaProductos);
        }

        [HttpPost]
        public ActionResult AgregarAlCarrito(int idArticulo, string nombre, int cantidad, decimal precio)
        {
            int userId = (int)Session["CompradorID"];

            string mensaje = comprador.AgregarAlCarrito(idArticulo, nombre, cantidad, precio, userId);

            return RedirectToAction("CatalogoArticulos");
        }



        public ActionResult Detalles(int id)
        {
            productos producto = comprador.ObtenerProductoPorId(id);
            return View(producto);
        }

        public ActionResult VerCarrito()
        {
            int userId = (int)Session["CompradorID"];
            List<carrito> carrito = comprador.ObtenerCarrito(userId); 

            decimal totalGeneral = carrito.Sum(p => p.total);

            ViewBag.TotalGeneral = totalGeneral;

            return View(carrito);
        }

        public ActionResult RealizarCompra()
        {
            int userId = (int)Session["CompradorID"];
            List<carrito> carrito = comprador.ObtenerCarrito(userId);

            decimal totalGeneral = carrito.Sum(p => p.total);
            comprador.RealizarCompra(carrito, totalGeneral, userId);
            comprador.LimpiarCarrito(userId);

            return RedirectToAction("Index", "Home");
        }



        public ActionResult VerPedidos()
        {
            int userId = (int)Session["CompradorID"];

            List<pedidos> pedidos = comprador.VerPedidos(userId);
            return View(pedidos);
        }













    }

}
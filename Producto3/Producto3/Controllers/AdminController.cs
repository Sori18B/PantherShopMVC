using Producto3.Logica;
using Producto3.Logica.Filtros;
using Producto3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Producto3.Controllers
{

    [SepararSesionesFilter]
    public class AdminController : Controller
    {
        Datos objDatos = new Datos();
        funcionalidades funcionalidades = new funcionalidades();
        Sesiones sesiones = new Sesiones();

        // GET: Admin
        public ActionResult IndexAdmin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistroAdmin()
        {
            return View();

        }
        [HttpPost]
        public ActionResult RegistroAdmin(usuarios nuevoUsuario)
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

        public ActionResult RegistroExitosoAdmin()
        {
            return View();
        }



        //-------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult IniciarSesionAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesionAdmin(usuarios modelo)
        {
            if (ModelState.IsValid)
            {
                string mensaje = new Sesiones().IniciarSesion(modelo.correo, modelo.contra);

                if (mensaje == "Inicio de sesión exitoso")
                {
                    int idUsuario = sesiones.ObtenerIdUsuario(modelo.correo, modelo.contra);
                    Session["AdministradorID"] = idUsuario;

                    return RedirectToAction("IndexAdmin", "Admin");
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
            // Eliminar la sesión del usuario
            Session.Clear();

            return RedirectToAction("IndexAdmin", "Admin");
        }

        //-------------------------------------------------------------------------------------

        public ActionResult Funciones()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AltaProducto()
        {
            return View(new productos());
        }

        [HttpPost]
        public ActionResult AltaProducto(productos producto)
        {
            if (ModelState.IsValid)
            {
                string mensaje = new funcionalidades().RegistraProducto(producto);

                if (mensaje == "El producto ha sido registrado")
                {
                    return RedirectToAction("ListadoProductos", "Admin");
                }

                ViewBag.Mensaje = mensaje;
            }

            return View(producto);
        }
        public ActionResult ListadoProductos()
        {
            funcionalidades funcionalidades = new funcionalidades(); 
            List<productos> listaProductos = funcionalidades.ObtenerListaProductos(); 
            return View(listaProductos);
        }

        public ActionResult VerProductos()
        {
            funcionalidades funcionalidades = new funcionalidades();
            List<productos> listaProductos = funcionalidades.Obtener3Productos();
            return View(listaProductos);
        }


        [HttpGet]
        public ActionResult ConfirmarEliminacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmarEliminacion(int idArt)
        {
            funcionalidades funcionalidades = new funcionalidades();

            funcionalidades.EliminarProducto(idArt);
            return RedirectToAction("ListadoProductos");
        }

        public ActionResult ListadoUsuarios()
        {
            funcionalidades funcionalidades = new funcionalidades();
            List<usuarios> listaUsuarios = funcionalidades.ObtenerListaUsuarios();
            return View(listaUsuarios);
        }

        // -------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult ModificarProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModificarProducto(int idArt, string nuevoNombre, string nuevaDescripcion, decimal nuevoPrecio)
        {
            funcionalidades funcionalidades = new funcionalidades();

            string mensaje = funcionalidades.ModificarProducto(idArt, nuevoNombre, nuevaDescripcion, nuevoPrecio);
            TempData["Mensaje"] = mensaje; 
            return RedirectToAction("ListadoProductos");
        }

        //---------------------------------------------------------------------------------------------

        public ActionResult VerPedidosPorUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerPedidosPorUsuario(int userId)
        {
            if (userId > 0)
            {
                return RedirectToAction("VerPedidos", "Admin", new { userId = userId });
            }
            else
            {
                ModelState.AddModelError("userId", "Por favor ingrese un ID de usuario válido.");
                return View();
            }
        }

        public ActionResult VerPedidos(int userId)
        {
            List<pedidos> pedidos = funcionalidades.VerPedidos(userId);
            return View(pedidos);
        }

        public ActionResult ClientesPorArticulo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientesPorArticulo(int idArticulo)
        {
            if (idArticulo > 0)
            {
                return RedirectToAction("ListadoClientesPorArticulo", "Admin", new { idArticulo = idArticulo });
            }
            else
            {
                ModelState.AddModelError("idArticulo", "Por favor ingrese un ID de artículo válido.");
                return View();
            }
        }

        public ActionResult ListadoClientesPorArticulo(int idArticulo)
        {
            List<ClientePorArticulo> clientesPorArticulo = funcionalidades.ObtenerClientesPorArticulo(idArticulo);
            return View(clientesPorArticulo);
        }

























    }
}
using System.Web.Mvc;
using Producto3.Controllers;

namespace Producto3.Logica.Filtros
{
    public class SepararSesionesFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.IsInRole("Comprador") && filterContext.Controller.GetType() != typeof(HomeController))
            {
                filterContext.HttpContext.Session.Clear();
                filterContext.Result = new RedirectResult("/Home/IniciarSesion");
                return;
            }
            else if (filterContext.HttpContext.User.IsInRole("Administrador") && filterContext.Controller.GetType() != typeof(AdminController))
            {
                filterContext.HttpContext.Session.Clear();
                filterContext.Result = new RedirectResult("/Admin/IniciarSesionAdmin");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}

using Producto3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Producto3.Logica.Filtros
{
    public class AdminFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName == "IniciarSesion")
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (!filterContext.HttpContext.User.IsInRole("Administrador"))
            {
                filterContext.HttpContext.Session.Clear();
                filterContext.Result = new RedirectResult("/Home/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}



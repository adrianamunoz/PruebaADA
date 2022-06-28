using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba.Permisos
{
    public class ValidarSesion: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(HttpContext.Current.Session["Usuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/UsuarioPrueba/Login");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
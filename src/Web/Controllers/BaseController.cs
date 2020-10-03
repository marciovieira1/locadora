using Locadora.Domain;
using Locadora.Web.Helpers;
using Simple.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Controllers
{
    public partial class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            //    if (filterContext.HttpContext.IsCustomErrorEnabled && !(filterContext.Exception is AuthorizationException))
            //    {
            //        filterContext.ExceptionHandled = true;
            //        var code = filterContext.Exception.GetType().Name;
            //        var message = filterContext.Exception.Message;
            //        var stackTrace = "";
            //        while (filterContext.Exception != null)
            //        {
            //            stackTrace += filterContext.Exception.GetType().Name + "\n" + filterContext.Exception.StackTrace + "\n\n";
            //            filterContext.Exception = filterContext.Exception.InnerException;
            //        }
            //        ViewBag.Code = code;
            //        ViewBag.Message = message;
            //        ViewBag.StackTrace = stackTrace;
            //        this.View("erro", (TUser)ViewBag.Usuario).ExecuteResult(this.ControllerContext);
            //    }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //    var user = TUser.FindByUsername(User.Identity.Name);
            //    var enabledAccess = TParameter.FindByEnum(Parameter.BlockAccessAdmin) == null;
            //    if (User.Identity.IsAuthenticated && user != null && (enabledAccess || user.Username == "admin"))
            //    {
            //        ViewBag.Usuario = user;
            //        ViewBag.Permissoes = user.ListPermissions();
            //    }
            //    else
            //        FormsAuthentication.SignOut();

            //    if (user != null && filterContext.HttpContext.Session != null)
            //    {
            //        if (filterContext.HttpContext.Session["MenuLateral"] != null)
            //            user.ActiveSideBarMenu = filterContext.HttpContext.Session["MenuLateral"].ToString() == "aberto";
            //        else
            //            user.ActiveSideBarMenu = true;
            //    }
            var cliente = TClient.FindByUsername(User.Identity.Name);
            if (User.Identity.IsAuthenticated && cliente != null)
            {
                ViewBag.Cliente = cliente;
            }
            else
                FormsAuthentication.SignOut();
            ViewBag.Alerta = TempData["Alerta"];
        }

        protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
        {
            ModelState.Clear();
            foreach (var item in ex.Errors)
                ModelState.AddModelError(item.PropertyName, item.Message);
            return View(model);
        }
    }
}
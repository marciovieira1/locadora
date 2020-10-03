using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Locadora.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RequiresAuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var uri = filterContext.HttpContext.Request.Url.AbsoluteUri;
            SecurityContext.Do.Init(() => filterContext.HttpContext.User.Identity);

            var security = SecurityContext.Do;
            if (security == null)
            {
                NotLogged(filterContext);
                return;
            }
            if (!security.IsAuthenticated)
            {
                NotLogged(filterContext);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private static void NotLogged(ActionExecutingContext filterContext)
        {
            var rUrl = "";
            if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.QueryString["returnUrl"]))
                rUrl = filterContext.HttpContext.Request.QueryString["returnUrl"].Replace("/Web_deploy", "");
            else
                rUrl = filterContext.HttpContext.Request.Url.AbsoluteUri.Replace("/Web_deploy", "");

            if (rUrl.Contains("painel"))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Painel" }, { "returnUrl", rUrl } });
            else if (rUrl.Contains("pdv"))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Pdv" }, { "returnUrl", rUrl } });
            else if (rUrl.Contains("ev"))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Ev" }, { "returnUrl", rUrl } });
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Clientes" }, { "Action", "Login" }, { "Area", "" }, { "returnUrl", rUrl } });
        }

        private static void NotAuthorizated(ActionExecutingContext filterContext)
        {
            filterContext.Result = NotAuthorizated(filterContext.HttpContext.Request.Url.AbsoluteUri.Replace("/Web_deploy", ""));
        }

        private static void NotAuthorizated(ExceptionContext filterContext)
        {
            filterContext.Result = NotAuthorizated(filterContext.HttpContext.Request.Url.AbsoluteUri.Replace("/Web_deploy", ""));
        }

        private static RedirectToRouteResult NotAuthorizated(string url)
        {
            if (url.Contains("painel"))
                return new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "acesso-negado" }, { "Area", "Painel" } });
            else if (url.Contains("pdv"))
                return new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "acesso-negado" }, { "Area", "Pdv" } });
            else if (url.Contains("ev"))
                return new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "conta-desativada" }, { "Area", "Ev" } });
            else
                return new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Clientes" }, { "Action", "Login" }, { "Area", "Loja" } });
        }


        public void OnAuthorization(AuthorizationContext filterContext)
        {
        }

        #region IExceptionFilter Members

        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is AuthorizationException)
                NotAuthorizated(exceptionContext);
        }

        #endregion
    }

}
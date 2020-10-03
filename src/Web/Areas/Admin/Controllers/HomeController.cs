using Locadora.Domain;
using Locadora.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class HomeController : BaseController
    {
        public virtual ActionResult Login(string returnUrl)
        {
            var login = new Login();
            ViewBag.ReturnUrl = returnUrl;
            return View(login);
        }
        [HttpPost]
        public virtual ActionResult Login(Login model)
        {
            model.NeedsToBeActive = false;
            var membro = TUser.Authenticate(model);
            if (membro != null)
            {
                FormsAuthentication.SetAuthCookie(membro.Login, false);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(membro.Login), new string[] { });
                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                TempData["Alerta"] = new Alert("success", "Bem-vindo" + " " + model.Username);

                return RedirectToAction(MVC.Admin.Home.Index());
            }
            else
            {
                membro = TUser.FindByUsername(model.Username);
                if (membro != null)
                {
                    TempData["Alerta"] = new Alert("error", "Senha incorreta");
                }
                TempData["Alerta"] = new Alert("error", "Usuário não existe");
                return View(model);
            }
        }
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            if (HttpContext.Session != null)
            {
                HttpContext.Session["ReadOnly"] = null;
                HttpContext.Session["AdminAccess"] = null;
                HttpContext.Session["Layout"] = null;
            }
            return RedirectToAction(MVC.Admin.Home.Login());
        }
        [RequiresAuthorization]
        public virtual ActionResult Index()
        {
            return View();
        }
 
    }
}
using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Controllers
{
    public partial class ClientesController : BaseController
    {
        public virtual ActionResult ListarFilmes(int id)
        {
            var filmes = TMovie.Load(id);
            return PartialView("_listar-filmes", filmes);
        }

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
            var membro = TClient.Authenticate(model);
            if (membro != null)
            {
                FormsAuthentication.SetAuthCookie(membro.Login, false);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(membro.Login), new string[] { });
                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                TempData["Alerta"] = new Alert("success", "Bem-vindo" + " " + model.Username);

                return RedirectToAction(MVC.Clientes.cliente_filmes());
            }
            else
            {
                membro = TClient.FindByUsername(model.Username);
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
            return RedirectToAction(MVC.Clientes.Login());
        }
    
        [RequiresAuthorization]
        public virtual ActionResult cliente_filmes()
        {
            List<TMovie> filmes;
            var client = (TClient)ViewBag.Cliente;
            var preferencias = client.TPreferences.Select(x => x.Category.Id).ToList();

            if (preferencias.Count != 0)
            {
                filmes = TMovieCategory.List(x => preferencias.Contains(x.Category.Id)).Select(x => x.Movie).ToList();
            }
            else
            {
                filmes = TMovie.ListAll().ToList();
            }
            return View(filmes);
        }
        public virtual ActionResult Reservar()
        {
            var reserva = new TReservation();
            ViewBag.Movie = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(reserva);
        }
        [HttpPost]
        public virtual ActionResult Reservar(TReservation model)
        {
            try
            {
                model.Save();
                TIten.SaveMovies(model);
                TempData["Alerta"] = new Alert("success", "Reserva realizada com sucesso");
                return RedirectToAction("cliente_filmes");
            }
            catch (SimpleValidationException ex)
            {
                return HandleViewException(model, ex);
            }
        }
        public ActionResult GetMovies(string term)
        {
            return Json(TMovie.List(x => x.Name.Contains(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
        }
    }
}
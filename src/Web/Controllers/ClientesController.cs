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
                    //return RedirectToAction(MVC.Clientes.Index());
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


        public virtual ActionResult Index()
        {
            var clientes = TClient.ListAll().ToList();
            return View(clientes);
        }

        public virtual ActionResult Cadastrar()
        {
            var cliente = new TClient();
            ViewBag.MostraSenha = true;
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(cliente);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TClient model)
        {
            try
            {
                model.Password = TClient.HashPassword(model.PasswordString);
                model.Save();
                TPreference.SavePreference(model);
                TempData["Alerta"] = new Alert("success", "Cliente cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = true;
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Editar(int id)
        {
            var cliente = TClient.Load(id);
            var preferencias = cliente.TPreferences.Select(x => x.Category.Id).ToList();
            ViewBag.MostraSenha = false;
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
            if (preferencias.Count != 0)
            {
                ViewBag.Category = TCategory.List(x => !preferencias.Contains(x.Id)).ToSelectList(x => x.Id, x => x.Name);
            }
            else
            {
                ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            }
            return View(cliente);
        }

        [HttpPost]
        public virtual ActionResult Editar(TClient model)
        {
            try
            {
                model.Edit();
                TPreference.SavePreference(model);
                TempData["Alerta"] = new Alert("success", "Cliente alterado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = false;
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Excluir(int id)
        {
            var cliente = TClient.Load(id);
            return PartialView("_excluir", cliente);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TPreference.Delete(x => x.Client.Id == id);
            TClient.Delete(id);
            TempData["Alerta"] = new Alert("success", "Cliente excluído com sucesso");
            return RedirectToAction("Index");
        }

        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-genero", genero);
        }

        [RequiresAuthorization]
        public virtual ActionResult cliente_filmes()
        {
            List<TMovie> filmes;
            var client = (TClient)ViewBag.Cliente;
            var preferencias = client.TPreferences.Select(x => x.Category.Id).ToList(); // Select id_category from t_preferences where id_clientes = 94

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
    }
}
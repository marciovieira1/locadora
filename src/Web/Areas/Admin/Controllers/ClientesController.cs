using Locadora.Domain;
using Locadora.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class ClientesController : BaseController
    {
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
    }
}
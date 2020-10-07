using Locadora.Domain;
using Locadora.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class UsuariosController : BaseController
    {
        public virtual ActionResult Index()
        {
            var usuarios = TUser.ListAll().ToList();
            return View(usuarios);
        }

        public virtual ActionResult Cadastrar()
        {
            var usuario = new TUser();
            ViewBag.MostraSenha = true;
            ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
            return View(usuario);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TUser model)
        {
            try
            {
                model.Password = TClient.HashPassword(model.PasswordString);
                model.Save();
                TempData["Alerta"] = new Alert("success", "Usuário cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = true;
                ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Editar(int id)
        {
            var usuario = TUser.Load(id);
            ViewBag.MostraSenha = false;
            ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
            return View(usuario);
        }

        [HttpPost]
        public virtual ActionResult Editar(TUser model)
        {
            try
            {
                model.Edit();
                TempData["Alerta"] = new Alert("success", "Usuário alterado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = false;
                ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Excluir(int id)
        {
            var usuario = TUser.Load(id);
            return PartialView("_excluir", usuario);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TUser.Delete(id);
            TempData["Alerta"] = new Alert("success", "Usuário excluído com sucesso");
            return RedirectToAction("Index");
        }

    }
}
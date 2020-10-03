using Locadora.Domain;
using Locadora.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Controllers
{
    public partial class GenerosController : BaseController
    {
        public virtual ActionResult Index()
        {
            var categoria = TCategory.ListAll().ToList();
            return View(categoria);
        }

        public virtual ActionResult Cadastrar()
        {
            var categoria = new TCategory();
            return View(categoria);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TCategory model)
        {
            try
            {
                model.Save();
                TempData["Alerta"] = new Alert("success", "Gênero cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Editar(int id)
        {
            var categoria = TCategory.Load(id);
            return View(categoria);
        }

        [HttpPost]
        public virtual ActionResult Editar(TCategory model)
        {
            model.Update();
            TempData["Alerta"] = new Alert("success", "Gênero alterado com sucesso");
            return RedirectToAction("Index");
        }
        public virtual ActionResult Excluir(int id)
        {
            var filme = TCategory.Load(id);
            return PartialView("_excluir", filme);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TCategory.Delete(id);
            TempData["Alerta"] = new Alert("success", "Gênero excluído com sucesso");
            return RedirectToAction("Index");
        }
    }
}
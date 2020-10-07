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
    public partial class FilmesController : BaseController
    {
        public virtual ActionResult Index()
        {
            var filmes = TMovie.ListAll().ToList();
            return View(filmes);
        }

        public virtual ActionResult Cadastrar()
        {
            var filme = new TMovie();
            ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(filme);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TMovie model)
        {
            try
            {
                model.Date = DateTime.Now.GetCurrent();
                model.Save();
                TMovieCategory.SaveCategories(model);
                TempData["Alerta"] = new Alert("success", "Filme cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
                ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Editar(int id)
        {
            var filme = TMovie.Load(id);
            var categoriasFilme = filme.TMovieCategories.Select(x => x.Category.Id).ToList();
            ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.Category = TCategory.List(x => !categoriasFilme.Contains(x.Id)).ToSelectList(x => x.Id, x => x.Name);
            return View(filme);
        }

        [HttpPost]
        public virtual ActionResult Editar(TMovie model)
        {
            model.Update();
            TMovieCategory.SaveCategories(model);
            TempData["Alerta"] = new Alert("success", "Filme alterado com sucesso");
            return RedirectToAction("Index");
        }

        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-genero", genero);
        }

        public virtual ActionResult Excluir(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_excluir", filme);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TMovieCategory.Delete(x => x.Movie.Id == id);
            TMovie.Delete(id);
            TempData["Alerta"] = new Alert("success", "Filme excluído com sucesso");
            return RedirectToAction("Index");
        }

    }
}
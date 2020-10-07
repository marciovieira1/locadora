using Locadora.Domain;
using Locadora.Web.Helpers;
using Simple.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Admin.Controllers
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
            var usuario = TUser.FindByUsername(User.Identity.Name);
            if (User.Identity.IsAuthenticated && usuario != null)
            {
                ViewBag.Usuario = usuario;
            }
            else
                FormsAuthentication.SignOut();
            ViewBag.Alerta = TempData["Alerta"];


            //var aa = TClient.List(x => x.Login.EndsWith("almeida"));
            //var aa2 = TReservation.List(x => x.Client.Login.Contains("matheus") && x.Client.Name.Contains("matheus"));

            ///*LISTAR RESERVAS DOS CLIENTES COM NOME QUE COMEÇAM COM A LETRA P */
            //var reservas = TReservation.List(x => x.Client.Name.StartsWith("p"));

            ///*LISTAR FILMES QUE ESTÃO NA CATEGORIA 3 QUE TERMINAM COM LATRA O */
            //var filmes = TMovieCategory.List(x => x.Category.Id == 3 && x.Movie.Name.EndsWith("o")).Select(x => x.Movie);

            ///*LISTAR/SELECIONAR O ID DAS RESERVAS QUE TENHAM O FILME ID 5 */
            //var reservas2 = TIten.List(x => x.Movie.Id == 5).Select(x => x.Reservation.Id);

            ///*EXCLUIR AS RESERVAS DO FILME ID 10 E CLIENTE QUE O NOME CONTENHA 'JOAO' */
            ////var itens = TIten.List(x => x.Movie.Id == 10 && x.Reservation.Client.Name.Contains("joao"));
            ////TIten.Delete(x => itens.Select(y => y.Id).Contains(x.Id));
            ////TReservation.Delete(x => itens.Select(y => y.Reservation.Id).Contains(x.Id));

            //var reservasExcluir = TIten.List(x => x.Movie.Id == 10 && x.Reservation.Client.Name.Contains("joao")).Select(x => x.Reservation);
            //TIten.Delete(x => reservasExcluir.Select(y => y.Id).Contains(x.Reservation.Id));
            //TReservation.Delete(x => reservasExcluir.Select(y => y.Id).Contains(x.Id));

            ///*LISTAR/SELECIONAR O NOME DAS PREFERÊNCIAS DO CLIENTE ID 18 */
            //var nomePreferencias = TPreference.List(x => x.Client.Id == 18).Select(x => x.Category.Name);

            ///*LISTAR AS CATEGORIAS QUE NÃO TEM FILMES RESERVADOS */
            //var filmesComReserva = TIten.ListAll().Select(x => x.Movie);
            //TMovieCategory.List(x => !filmesComReserva.Select(y => y.Id).Contains(x.Movie.Id)).Select(x => x.Category);

            ///*LISTAR OS FILMES DA CATEGORIA 35 QUE TEM RESERVAS */
            //var filmesCategoria = TMovieCategory.List(x => x.Category.Id == 35).Select(x => x.Movie);
            //var itensFilmesCategoria = TIten.List(x => filmesCategoria.Select(y => y.Id).Contains(x.Movie.Id)).Select(x => x.Movie);

            ///*EXCLUIR OS CLIENTES QUE NÃO FIZERAM RESERVAS */
            //var clientesComReservas = TReservation.ListAll().Select(x => x.Client.Id);
            //TClient.Delete(x => !clientesComReservas.Contains(x.Id));

            ///*ATUALIZAR O NOME DE TODOS OS FILMES ACRESCENTANDO UM " - NOVO" NO FINAL, QUE PERTENCEM A CATEGORIA 25  */
            //var filmesCategoria2 = TMovieCategory.List(x => x.Category.Id == 25).Select(x => x.Movie);
            //foreach (var item in filmesCategoria2)
            //{
            //    item.Name += " - NOVO";
            //    item.Update();
            //}

            ///*ATUALIZAR AS RESERVAS DO CLIENTE ID 46 ACRESCENTANDO MAIS UM DIA NA DATA DE ENTREGA */
            //var reservasCliente = TReservation.List(x => x.Client.Id == 46);
            //foreach (var item in reservasCliente)
            //{
            //    item.Devolution = item.Devolution.AddDays(1);
            //    item.Update();
            //}
            ///*LISTAR/SELECIONAR AS QUANTIDADES OS ITENS DAS RESERVAS DOS CLIENTE QUE COMEÇAM COM A LETRA P */
            //TIten.List(x => x.Reservation.Client.Name.StartsWith("p")).Select(x => x.Quantity);
            //TIten.List(x => x.Reservation.Client.Name.StartsWith("p")).Sum(x => x.Quantity);

            ///*EXCLUIR OS ITENS DAS RESERVAS CUJO OS FILMES TERMINEM COM A LETRA M E A DATA DE RETIRADA DA RESERVA JÁ EXPIROU */
            //TIten.Delete(x => x.Movie.Name.EndsWith("m") && x.Reservation.Withdraw < DateTime.Now);
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
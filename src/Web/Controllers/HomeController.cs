
using System.Web.Mvc;
using Locadora.Domain;
using System.Web.Security;
using System.Security.Principal;
using Locadora.Web.Helpers;

namespace Locadora.Web.Controllers
{
    public partial class HomeController : BaseController
    {

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Realizar()
        {
            return View();
        }
    }
}

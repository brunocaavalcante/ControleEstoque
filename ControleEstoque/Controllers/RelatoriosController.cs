using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class RelatoriosController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult PosicaoEstoque()
        {
            return View();
        }
        [Authorize]
        public ActionResult Ressuprimento()
        {
            return View();
        }
    }
}
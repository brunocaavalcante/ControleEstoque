using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoqueWeb.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoqueWeb.Controllers
{
    public class GraficosController : Controller
    {
        // GET: Graficos
        [Authorize]
        public ActionResult PerdasMes()
        {
            return View();
        }
        [Authorize]
        public ActionResult EntradaSaidaMes()
        {
            return View();
        }
    }
}
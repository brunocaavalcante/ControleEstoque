using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoqueWeb.Controllers
{
    public class OperacoesController : Controller
    {
        [Authorize]
        public ActionResult EntradaEstoque()
        {
            return View();
        }
        [Authorize]
        public ActionResult SaidaEstoque()
        {
            return View();
        }
        [Authorize]
        public ActionResult LacamentoPerda()
        {
            return View();
        }
        [Authorize]
        public ActionResult Inventario()
        {
            return View();
        }
    }
}
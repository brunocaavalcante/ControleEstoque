using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class OperacoesController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
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
        public ActionResult LancPerdaProduto()
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
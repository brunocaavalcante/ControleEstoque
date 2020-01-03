using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ControleEstoque.Models;
using Newtonsoft.Json.Linq;

namespace ControleEstoque.Controllers
{
    public class CadastroController : Controller
    {
        #region Usuários

        private const string _senhaPadrao = "1112718822";

        [Authorize]
        public ActionResult Usuario()
        {
            ViewBag.SenhaPadrao = _senhaPadrao;
            return View(UsuarioModel.RecuperarLista());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(int id)
        {
            return Json(UsuarioModel.findUsuario(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int id)
        {
            return Json(UsuarioModel.deleteUsuario(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    if (model.Senha == _senhaPadrao)
                    {
                        model.Senha = "";
                    }

                    var id = model.salvarUsuario();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        #endregion

        #region MarcaProduto
        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }
        #endregion

        #region LocalProduto
        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }
        #endregion

        #region UnidadeMedida
        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }
        #endregion

        #region Produto
        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }
        #endregion

        #region Pais
        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }
        #endregion

        #region Ver depois
        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }

        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }
        #endregion
    }

}

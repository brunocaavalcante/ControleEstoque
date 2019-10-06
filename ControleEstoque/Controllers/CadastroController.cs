using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControleEstoque.Models;
namespace ControleEstoque.Controllers
{
    public class CadastroController : Controller
    {
        #region GrupoProduto
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]//Valida o token gerado pelo usuario, utilizamos isso para evitar ataques CRSF
        public ActionResult RecuperarGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.findGrupoProduto(id));
        }

        [HttpPost] //Tipo de requisição
        [Authorize]
        [ValidateAntiForgeryToken]//Valida o token gerado pelo usuario, utilizamos isso para evitar ataques CRSF
        public ActionResult ExcluirGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.deleteGrupoProduto(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] //Valida o token gerado pelo usuario, utilizamos isso para evitar ataques CRSF
        public ActionResult SalvarGrupoProduto(GrupoProdutoModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = "";
            Console.WriteLine("Passou aqui");

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();//Retorna os objetos que tiveram erros selecione muitos valores e transforma em lista
            }
            else
            {
                try
                {
                    var id = model.salvarGrupoProduto();

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

        [Authorize]
        public ActionResult GrupoProduto()
        {
            return View(GrupoProdutoModel.RecuperarLista());
        }
        #endregion

        #region Usuario
        [Authorize]
        public ActionResult Usuario()
        {
            return View();
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


        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }
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
        
    }
}
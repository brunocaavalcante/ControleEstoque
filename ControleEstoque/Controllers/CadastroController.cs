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

        #region GrupoProduto
        const int _quantidadeMaximaPorPage = 5;
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
            ViewBag.QtdMaximaLinhasPagina = 5; //Quando estamos fazendo paginação é necessario informar a quantidade de linhas da table será exibida no caso 5
            ViewBag.PaginaAtual = 1; //Aqui informamos a pagina selecionado quando o usuario faz um get a pagina inicial sempre será 1
            var lista = GrupoProdutoModel.RecuperarLista(ViewBag.PaginaAtual, ViewBag.QtdMaximaLinhasPagina);
            ViewBag.QuantPaginas = (lista.Count / ViewBag.QtdMaximaLinhasPagina);
            return View(lista);            
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult GrupoProdutoPagina(int pagina)
        {            
            var lista = GrupoProdutoModel.RecuperarLista(pagina, _quantidadeMaximaPorPage);
            ViewBag.QtdMaximaLinhasPagina = 5; //Quando estamos fazendo paginação é necessario informar a quantidade de linhas da table será exibida no caso 5
            ViewBag.PaginaAtual = 1; //Aqui informamos a pagina selecionado quando o usuario faz um get a pagina inicial sempre será 1
            ViewBag.QuantPaginas = (lista.Count / ViewBag.QtdMaximaLinhasPagina);
            return Json(lista);
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

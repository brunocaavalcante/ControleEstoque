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
        private static List<GrupoProdutoModel> _listaGrupoProduto = new List<GrupoProdutoModel>()
        {
            new GrupoProdutoModel() { Id=1, Nome="Livros", Ativo=true },
            new GrupoProdutoModel() { Id=2, Nome="Mouses", Ativo=true },
            new GrupoProdutoModel() { Id=3, Nome="Monitores", Ativo=false }
        };

        [HttpPost]
        [Authorize]
        public ActionResult RecuperarGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.findGrupoProduto(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExcluirGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.deleteGrupoProduto(id));
        }

        [HttpPost]
        [Authorize]
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
                    var registroBD = _listaGrupoProduto.Find(x => x.Id == model.Id);

                    if (registroBD == null)
                    {
                        registroBD = model;
                        registroBD.Id = _listaGrupoProduto.Max(x => x.Id) + 1;
                        _listaGrupoProduto.Add(registroBD);               
                    }
                    else
                    {
                        registroBD.Nome = model.Nome;
                        registroBD.Ativo = model.Ativo;                        
                    }
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
        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }
        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }
        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }
        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }
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
        [Authorize]
        public ActionResult Usuario()
        {
            return View();
        }


    }
}
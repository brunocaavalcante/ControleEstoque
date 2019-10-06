using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControleEstoque.Models;
using System.Web.Security;

namespace ControleEstoque.Controllers
{
    public class ContaController : Controller
    {
      
        [AllowAnonymous] /*Tornando o método Publico*/
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login,string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var usuario = UsuarioModel.ValidarUser(login.Usuario,login.Senha);

            if (usuario != null)
            {
               FormsAuthentication.SetAuthCookie(usuario.Nome, login.LembrarMe);/*Criando o Cookie de Autenticação*/

                if (Url.IsLocalUrl(returnUrl)) /*Verificando se a Url está dentro do dominio*/
                {
                    return Redirect(returnUrl);
                }
                else
                {
                   return RedirectToAction("Index", "Home"); /* Se não estiver no dominio redirecionamos para home*/
                }
            }
            else /* Se as crendenciais forem digitadas erradas mostramos uma Mensagem de Erro*/
            {
                ModelState.AddModelError("", "Login Invalido!");
            }
            return View(login);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
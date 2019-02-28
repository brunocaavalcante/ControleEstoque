using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;/*Por esse import podemos utilizar os validations*/
using System.Linq;
using System.Web;

namespace ControleEstoque.Models
{
    public class LoginViewModel
    {
        [Required (ErrorMessage ="Informe o Usuário!")]
        [Display(Name="Usuário:")]
        public string Usuario { get; set; }

        [Required (ErrorMessage ="Informe a Senha!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        [Display(Name = "Lembrar Me")]
        public bool LembrarMe { get; set; }
    }
}
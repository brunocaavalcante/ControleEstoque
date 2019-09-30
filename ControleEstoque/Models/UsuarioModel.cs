using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Models
{
    public class UsuarioModel
    {
        public static bool ValidarUser(String login, String senha)
        {
            var ret = false;
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; //associando o comando a nossa conexão
                    comando.CommandText = string.Format("select count(*) from usuario where login = '{0}' and senha = '{1}'", login, senha);
                    ret = ((int)comando.ExecuteScalar() > 0);

                }
            }
            return ret;
        }
    }
}

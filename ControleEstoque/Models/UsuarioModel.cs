using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
                    comando.CommandText = "select count(*) from usuario where login = @login and senha = @senha";
                    //Adicionando os parametros login e senha
                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelp.HashMD5(senha);
                    ret = ((int)comando.ExecuteScalar() > 0);

                }
            }
            return ret;
        }
    }

}
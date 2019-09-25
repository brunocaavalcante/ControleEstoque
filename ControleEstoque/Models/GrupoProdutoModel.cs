using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Preencha o Nome!")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public static List<GrupoProdutoModel> RecuperarLista() 
        {
            var ret = new List<GrupoProdutoModel>();

            using (var conexao = new SqlConnection()) 
            {
                conexao.ConnectionString = @"Data Source=USER-PC;Initial Catalog=controle-estoque;User Id=brunoc;Password=123";
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from grupo_produto order by nome";
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new GrupoProdutoModel //Para cada objeto da lista retornado no ResultSet estamos atribuindo os valores do banco
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }

            return ret;
        }

        public static GrupoProdutoModel findGrupoProduto(int id)
        {
            GrupoProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = @"Data Source=USER-PC;Initial Catalog=controle-estoque;User Id=brunoc;Password=123";
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select * from grupo_produto where id = {0}",id);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret = new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        };
                    }
                }
            }

            return ret;
        }

        public static bool deleteGrupoProduto(int id)
        {
            bool ret = false;
            if (findGrupoProduto(id) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = @"Data Source=USER-PC;Initial Catalog=controle-estoque;User Id=brunoc;Password=123";
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format("delete from grupo_produto where id = {0}", id);
                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }
            return ret;
        }
    }
}
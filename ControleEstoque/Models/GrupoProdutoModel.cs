using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o Nome!")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public static List<GrupoProdutoModel> RecuperarLista(int pagina, int tamanho)
        {
            var ret = new List<GrupoProdutoModel>();
            int ini = (pagina - 1) * tamanho;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select * from grupo_produto order by nome offset {0} rows fetch next {1} rows only", ini > 0 ? ini - 1 : ini, tamanho);
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
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from grupo_produto where id = @id";
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
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
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "delete from grupo_produto where id = @id";
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }
            return ret;
        }

        public int salvarGrupoProduto()
        {
            int ret = 0;
            var model = findGrupoProduto(this.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    if (model == null)
                    { //Case Inserção
                        comando.CommandText = "insert into grupo_produto(nome,ativo) values (@nome,@ativo);select convert(int,scope_identity())"; //scope_identity retorna o id do ultimo valor inserido na tabela
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = this.Ativo ? 1 : 0;
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    { //Case Atualização
                        comando.CommandText = "update grupo_produto set nome = @nome, ativo = @ativo where id = @id";
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = this.Ativo ? 1 : 0;
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }
                }

                return ret;
            }
        }

        public static int QuantTotal()
        {
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                int quantidade;
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select count(*) from grupo_produto";
                    quantidade = (int)comando.ExecuteScalar();
                }

                return quantidade;
            }
        }
    }
}
using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Aluno_AtividadeDAO : IGenericDAO<Aluno_AtividadeDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(Aluno_AtividadeDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idAluno_Disciplina == 0)
                    cmd.CommandText = "insert into aluno_atividade(nota, aluno, atividade) values (@nota, @aluno, @atividade);";
                else
                {
                    cmd.CommandText = "update aluno_atividade set nota = @nota, aluno = @aluno, atividade = @atividade where idaluno_atividade = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idAluno_Disciplina);
                }
                cmd.Parameters.AddWithValue("@nota", obj.nota);
                cmd.Parameters.AddWithValue("@aluno", obj.aluno.idUsuario);
                cmd.Parameters.AddWithValue("@atividade", obj.atividade.idAtividade);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro na DAO Aluno_Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<Aluno_AtividadeDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<Aluno_AtividadeDTO> lista = new List<Aluno_AtividadeDTO>();
            Aluno_AtividadeDTO aluno_atividade = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select aat.idAluno_Atividade, u.nomeUsuario, at.descricaoAtividade, aat.nota from aluno a inner join " + 
                "usuario u on u.idUsuario = a.usuario inner join aluno_atividade aat on aat.aluno = a.idAluno " + 
                "inner join atividade at on aat.atividade = at.idatividade";
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    aluno_atividade = new Aluno_AtividadeDTO();
                    aluno_atividade.idAluno_Disciplina = Convert.ToInt32(resul["idAluno_atividade"]);
                    aluno_atividade.nota = Convert.ToInt16(resul["nota"]);
                    aluno_atividade.aluno = new AlunoDTO(0, resul["nomeUsuario"].ToString());
                    aluno_atividade.atividade = new AtividadeDTO(0, resul["descricaoAtividade"].ToString());
                    lista.Add(aluno_atividade);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Aluno_Atividade! Erro: " + ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro na DAO Aluno_Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public Aluno_AtividadeDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            Aluno_AtividadeDTO aluno_atividade = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from aluno_atividade where idAluno_atividade = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    aluno_atividade = new Aluno_AtividadeDTO();
                    aluno_atividade.idAluno_Disciplina = Convert.ToInt32(resul["idaluno_atividade"]);
                    aluno_atividade.nota = Convert.ToInt16(resul["nota"]);
                    aluno_atividade.aluno = new AlunoDTO(Convert.ToInt32(resul["aluno"]),null);
                    aluno_atividade.atividade = new AtividadeDTO(Convert.ToInt32(resul["atividade"]), null); 
                }
                return aluno_atividade;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Aluno_Atividade! Erro: " + ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro na DAO Aluno_Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public bool excluir(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from aluno_atividade where idaluno_atividade = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro na DAO Aluno_Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

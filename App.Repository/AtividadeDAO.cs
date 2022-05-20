using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class AtividadeDAO : IGenericDAO<AtividadeDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(AtividadeDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idAtividade == 0)
                {
                    if(obj.dataFinalAtividade != null)
                    {
                        cmd.CommandText = "insert into atividade(descricaoAtividade, dataInicioAtividade, dataFinalAtividade, disciplina) values" + 
                        "(@descricao, @dataInicial, @dataFinal, @disciplina)";
                        cmd.Parameters.AddWithValue("@dataFinal", obj.dataFinalAtividade);
                    }
                    else
                    {
                        cmd.CommandText = "insert into atividade(descricaoAtividade, dataInicioAtividade, disciplina) values" +
                        "(@descricao, @dataInicial, @disciplina)";
                    }
                }
                else
                {
                    if (obj.dataFinalAtividade != null)
                    {
                        cmd.CommandText = "update atividade set descricaoAtividade = @descricao, dataInicioAtividade = @dataInicial, " +
                        "dataFinalAtividade = @dataFinal, disciplina = @disciplina where idAtividade = @id";
                        cmd.Parameters.AddWithValue("@dataFinal", obj.dataFinalAtividade);
                    }
                    else
                    {
                        cmd.CommandText = "update atividade set descricaoAtividade = @descricao, dataInicioAtividade = @dataInicial, dataFinalAtividade = NULL, " +
                        "disciplina = @disciplina where idAtividade = @id";
                    }
                    cmd.Parameters.AddWithValue("@id", obj.idAtividade);
                }
                cmd.Parameters.AddWithValue("@descricao", obj.descricaoAtividade);
                cmd.Parameters.AddWithValue("@dataInicial", obj.dataInicioAtividade);
                cmd.Parameters.AddWithValue("@disciplina", obj.disciplina.idDisciplina);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Atividade! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Atividade ao fechar os parâmetros de conexao! Erro: " + ex.Message);
                }
            }
        }

        public List<AtividadeDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<AtividadeDTO> lista = new List<AtividadeDTO>();
            AtividadeDTO atividade = new AtividadeDTO();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select Convert(char, a.dataInicioAtividade, 103) as dataInicial, Convert(char, a.dataFinalAtividade, 103) as dataFinal, a.idAtividade, a.descricaoAtividade, d.descricaoDisciplina from atividade a inner join disciplina d on d.idDisciplina = a.disciplina";
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    atividade = new AtividadeDTO();
                    atividade.idAtividade = Convert.ToInt32(resul["idAtividade"]);
                    atividade.descricaoAtividade = resul["descricaoAtividade"].ToString();
                    atividade.dataInicioAtividade = Convert.ToDateTime(resul["dataInicial"]);
                    if(!string.IsNullOrEmpty(resul["dataFinal"].ToString()))
                        atividade.dataFinalAtividade = Convert.ToDateTime(resul["dataFinal"]);
                    atividade.disciplina = new DisciplinaDTO(0, resul["descricaoDisciplina"].ToString());
                    lista.Add(atividade);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Atividade! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public AtividadeDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            AtividadeDTO atividade = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select Convert(char, a.dataInicioAtividade, 103) as dataInicial, " + 
                    "Convert(char, a.dataFinalAtividade, 103) as dataFinal, a.idAtividade, " + 
                    "a.descricaoAtividade, d.idDisciplina from atividade a " + 
                    "inner join disciplina d on d.idDisciplina = a.disciplina where a.idAtividade = @id";

                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    atividade = new AtividadeDTO();
                    atividade.idAtividade = Convert.ToInt32(resul["idAtividade"]);
                    atividade.descricaoAtividade = resul["descricaoAtividade"].ToString();
                    atividade.dataInicioAtividade = Convert.ToDateTime(resul["dataInicial"]);
                    if(!string.IsNullOrEmpty(resul["dataFinal"].ToString()))
                        atividade.dataFinalAtividade = Convert.ToDateTime(resul["dataFinal"]);
                    atividade.disciplina = new DisciplinaDTO(Convert.ToInt32(resul["idDisciplina"]), null);
                }
                return atividade;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Atividade! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from atividade where idAtividade = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Atividade! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Atividade ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

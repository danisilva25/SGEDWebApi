using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Turma_DisciplinaDAO : IGenericDAO<Turma_DisciplinaDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(Turma_DisciplinaDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idTurma_Disciplina == 0)
                    cmd.CommandText = "insert into turma_disciplina(turma, disciplina) values(@turma, @disciplina);";
                else
                {
                    cmd.CommandText = "update turma_disciplina set turma = @turma, disciplina = @disciplina where idturma_disciplina = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma_Disciplina);
                }
                cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                cmd.Parameters.AddWithValue("@disciplina", obj.disciplina.idDisciplina);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<Turma_DisciplinaDTO> listar(string busca)
        {
            List<Turma_DisciplinaDTO> lista = new List<Turma_DisciplinaDTO>();
            Turma_DisciplinaDTO turma_disciplina = null;
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select td.idTurma_disciplina, t.descricaoTurma, d.descricaoDisciplina from turma_disciplina td " +
                "inner join turma t on t.idTurma = td.turma inner join disciplina d on d.idDisciplina = td.disciplina;";
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    turma_disciplina = new Turma_DisciplinaDTO();
                    turma_disciplina.idTurma_Disciplina = Convert.ToInt32(resul["idturma_disciplina"]);
                    turma_disciplina.turma = new TurmaDTO(0, resul["descricaoTurma"].ToString());
                    turma_disciplina.disciplina = new DisciplinaDTO(0, resul["descricaoDisciplina"].ToString());
                    lista.Add(turma_disciplina);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public Turma_DisciplinaDTO carregar(int idObject)
        {
            Turma_DisciplinaDTO turma_disciplina = null;
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from Turma_disciplina where idturma_disciplina = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    turma_disciplina = new Turma_DisciplinaDTO();
                    turma_disciplina.idTurma_Disciplina = Convert.ToInt32(resul["idturma_disciplina"]);
                    turma_disciplina.disciplina = new DisciplinaDTO(Convert.ToInt32(resul["disciplina"]), null);
                    turma_disciplina.turma = new TurmaDTO(Convert.ToInt32(resul["turma"]), null);
                }
                return turma_disciplina;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Disciplina ao fechar osparâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from turma_disciplina where idturma_disciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

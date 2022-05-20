using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Professor_DisciplinaDAO : IGenericDAO<Professor_DisciplinaDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(Professor_DisciplinaDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idProfessor_Disciplina == 0)
                    cmd.CommandText = "insert into professor_disciplina(professor, disciplina) values(@professor, @disciplina)";
                else
                {
                    cmd.CommandText = "update professor_disciplina set professor = @professor, disciplina = @disciplina where idprofessor_disciplina = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idProfessor_Disciplina);
                }
                cmd.Parameters.AddWithValue("@professor", obj.professor.idUsuario);
                cmd.Parameters.AddWithValue("@disciplina", obj.disciplina.idDisciplina);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Professor_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<Professor_DisciplinaDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            Professor_DisciplinaDTO professor_disciplina = null;
            List<Professor_DisciplinaDTO> lista = new List<Professor_DisciplinaDTO>();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select pd.idprofessor_disciplina, p.idProfessor, u.nomeUsuario, d.descricaoDisciplina from professor p " + 
                "inner join usuario u on u.idUsuario = p.usuario inner join professor_disciplina pd on pd.professor = p.idprofessor " + 
                "inner join disciplina d on d.idDisciplina = pd.disciplina";
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    professor_disciplina = new Professor_DisciplinaDTO();
                    professor_disciplina.idProfessor_Disciplina = Convert.ToInt32(resul["idprofessor_disciplina"]);
                    professor_disciplina.professor = new ProfessorDTO(0, resul["nomeusuario"].ToString());
                    professor_disciplina.disciplina = new DisciplinaDTO(0, resul["descricaoDisciplina"].ToString());
                    lista.Add(professor_disciplina);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Professor_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public Professor_DisciplinaDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            Professor_DisciplinaDTO professor_disicplina = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from professor_disciplina where idprofessor_disciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    professor_disicplina = new Professor_DisciplinaDTO();
                    professor_disicplina.idProfessor_Disciplina = Convert.ToInt32(resul["idprofessor_disciplina"]);
                    professor_disicplina.professor = new ProfessorDTO(Convert.ToInt32(resul["professor"]), null);
                    professor_disicplina.disciplina = new DisciplinaDTO(Convert.ToInt32(resul["disciplina"]), null);
                }
                return professor_disicplina;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAo ao carregar Professor_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from professor_disciplina where idprofessor_disciplina = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Professor_Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

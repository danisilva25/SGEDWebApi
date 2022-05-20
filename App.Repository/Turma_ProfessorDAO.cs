using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Turma_ProfessorDAO : IGenericDAO<Turma_ProfessorDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(Turma_ProfessorDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idTurma_Professor == 0)
                    cmd.CommandText = "insert into turma_professor(turma, professor) values(@turma, @professor);";
                else
                {
                    cmd.CommandText = "update turma_professor set turma = @turma, professor = @professor where idTurma_professor = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma_Professor);
                }
                cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                cmd.Parameters.AddWithValue("@professor", obj.professor.idUsuario);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma_Professor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Professor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<Turma_ProfessorDTO> listar(string busca)
        {
            List<Turma_ProfessorDTO> lista = new List<Turma_ProfessorDTO>();
            Turma_ProfessorDTO turma_professor = null;
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select tp.idturma_professor, u.nomeUsuario, t.descricaoTurma from " +
                "professor p inner join usuario u on p.usuario = u.idUsuario inner join turma_professor tp on " + 
                "tp.professor = p.idProfessor inner join turma t on t.idTurma = tp.turma;";
                var resul = cmd.ExecuteReader();

                while(resul.Read())
                {
                    turma_professor = new Turma_ProfessorDTO();
                    turma_professor.idTurma_Professor = Convert.ToInt32(resul["idTurma_professor"]);
                    turma_professor.professor = new ProfessorDTO(0, resul["nomeUsuario"].ToString());
                    turma_professor.turma = new TurmaDTO(0, resul["descricaoTurma"].ToString());
                    lista.Add(turma_professor);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma_Professor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Professor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public Turma_ProfessorDTO carregar(int idObject)
        {
            Turma_ProfessorDTO turma_professor = null;
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from turma_professor where idturma_professor = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    turma_professor = new Turma_ProfessorDTO();
                    turma_professor.idTurma_Professor = Convert.ToInt32(resul["idTurma_professor"]);
                    turma_professor.turma = new TurmaDTO(Convert.ToInt32(resul["turma"]), null);
                    turma_professor.professor = new ProfessorDTO(Convert.ToInt32(resul["professor"]), null);
                }
                return turma_professor;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma_Professor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Professor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from turma_professor where idTurma_professor = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma_Professor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Professor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

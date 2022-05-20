using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Professor_EscolaDAO :IGenericDAO<Professor_EscolaDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(Professor_EscolaDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if(obj.idProfessor_Escola == 0)
                    cmd.CommandText = "insert into professor_escola(professor, escola) values(@professor, @escola);";
                else
                {
                    cmd.CommandText = "update professor_escola set professor = @professor, escola = @escola where idProfessor_escola = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idProfessor_Escola);
                }
                cmd.Parameters.AddWithValue("@professor", obj.professor.idUsuario);
                cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao inserir Professor_Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Escola ao fechar os paramêtros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<Professor_EscolaDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<Professor_EscolaDTO> lista = new List<Professor_EscolaDTO>();
            Professor_EscolaDTO professor_escola = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select pe.idprofessor_escola, u.nomeUsuario, e.nomeEscola from professor p "+
                    "inner join professor_escola pe on pe.professor = p.idProfessor " +
                    "inner join usuario u on u.idUsuario = p.usuario " +
                    "inner join escola e on e.idEscola = pe.escola";
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    professor_escola = new Professor_EscolaDTO();
                    professor_escola.idProfessor_Escola = Convert.ToInt32(resul["idprofessor_escola"]);
                    professor_escola.professor = new ProfessorDTO(0, resul["nomeUsuario"].ToString());
                    professor_escola.escola = new EscolaDTO(0, resul["nomeEscola"].ToString());
                    lista.Add(professor_escola);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Professor_Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public Professor_EscolaDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            Professor_EscolaDTO professor_escola = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select pe.idprofessor_escola, e.idEscola, p.idProfessor from professor p " +
                    "inner join professor_escola pe on pe.professor = p.idProfessor " +
                    "inner join escola e on e.idEscola = pe.escola where idprofessor_escola = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    professor_escola = new Professor_EscolaDTO();

                    professor_escola.idProfessor_Escola = Convert.ToInt32(resul["idprofessor_escola"]);
                    professor_escola.professor = new ProfessorDTO(Convert.ToInt32(resul["idProfessor"]), null);
                    professor_escola.escola = new EscolaDTO(Convert.ToInt32(resul["idEscola"]), null);
                }
                return professor_escola;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Professor_Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from professor_escola where idprofessor_escola = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Professor_Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor_Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

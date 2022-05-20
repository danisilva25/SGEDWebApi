using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class Turma_EscolaDAO : IGenericDAO<Turma_EscolaDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(Turma_EscolaDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idTurma_Escola == 0)
                    cmd.CommandText = "insert into turma_escola(turma, escola) values(@turma, @escola);";
                else
                {
                    cmd.CommandText = "update turma_escola set turma = @turma, escola = @escola where idTurma_Escola = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma_Escola);
                }
                cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma_Escola: Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<Turma_EscolaDTO> listar(string busca)
        {
            List<Turma_EscolaDTO> lista = new List<Turma_EscolaDTO>();
            Turma_EscolaDTO turma_escola = null;
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select te.idTurma_Escola, t.descricaoTurma, e.nomeEscola from turma_escola te " + 
                "inner join turma t on t.idTurma = te.turma inner join escola e on e.idEscola = te.escola";
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    turma_escola = new Turma_EscolaDTO();
                    turma_escola.idTurma_Escola = Convert.ToInt32(resul["idTurma_Escola"]);
                    turma_escola.escola = new EscolaDTO(0, resul["nomeEscola"].ToString());
                    turma_escola.turma = new TurmaDTO(0, resul["descricaoTurma"].ToString());
                    lista.Add(turma_escola);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma_Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public Turma_EscolaDTO carregar(int idObject)
        {
            Turma_EscolaDTO turma_escola = null;
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from turma_Escola where idTurma_escola = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    turma_escola = new Turma_EscolaDTO();
                    turma_escola.idTurma_Escola = Convert.ToInt32(resul["idTurma_Escola"].ToString());
                    turma_escola.escola = new EscolaDTO(Convert.ToInt32(resul["escola"]), null);
                    turma_escola.turma = new TurmaDTO(Convert.ToInt32(resul["turma"]), null);
                }
                return turma_escola;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma_Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from turma_escola where idturma_escola = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma_Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma_Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

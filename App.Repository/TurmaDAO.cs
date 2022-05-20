using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class TurmaDAO : IGenericDAO<TurmaDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(TurmaDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idTurma == 0)
                {
                    cmd.CommandText = "insert into turma(descricaoTurma) values(@descricao);";
                }
                else
                {
                    cmd.CommandText = "update turma set descricaoTurma = @descricao where idTurma = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idTurma);
                }
                cmd.Parameters.AddWithValue("@descricao", obj.descricaoTurma);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Turma! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<TurmaDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<TurmaDTO> listaTurmas = new List<TurmaDTO>();
            TurmaDTO turma = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select * from turma;";
                else
                    cmd.CommandText = "select * from turma where descricaoTurma like '%" + busca + ";";

                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    turma = new TurmaDTO();
                    turma.idTurma = int.Parse(resul["idTurma"].ToString());
                    turma.descricaoTurma = resul["descricaoTurma"].ToString();
                    listaTurmas.Add(turma);
                }
                return listaTurmas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Turma! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public TurmaDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            TurmaDTO turma = new TurmaDTO();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from turma where idTurma = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    turma.idTurma = int.Parse(resul["idTurma"].ToString());
                    turma.descricaoTurma = resul["descricaoTurma"].ToString();
                }
                return turma;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Turma! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from turma where idTurma = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Turma! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Turma ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

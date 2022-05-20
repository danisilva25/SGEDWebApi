using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class DisciplinaDAO : IGenericDAO<DisciplinaDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(DisciplinaDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idDisciplina == 0)
                {
                    cmd.CommandText = "insert into disciplina(descricaoDisciplina) values(@descricao);";
                }
                else
                {
                    cmd.CommandText = "update disciplina set descricaoDisciplina = @descricao where idDisciplina = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idDisciplina);
                }
                cmd.Parameters.AddWithValue("@descricao", obj.descricaoDisciplina);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<DisciplinaDTO> listar(string busca)
        {
            List<DisciplinaDTO> disciplinas = new List<DisciplinaDTO>();
            SqlConnection conn = new SqlConnection(stringdeconexao);
            DisciplinaDTO disciplinaDto = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select * from disciplina;";
                else
                {
                    cmd.CommandText = "select * from disciplina where descricaoDisciplina like '%" + busca + "';";
                }
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    disciplinaDto = new DisciplinaDTO();
                    disciplinaDto.idDisciplina = int.Parse(resul["idDisciplina"].ToString());
                    disciplinaDto.descricaoDisciplina = resul["descricaoDisciplina"].ToString();
                    disciplinas.Add(disciplinaDto);
                }
                return disciplinas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public DisciplinaDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            DisciplinaDTO disciplina = new DisciplinaDTO();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from disciplina where idDisciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    disciplina.idDisciplina = int.Parse(resul["idDisciplina"].ToString());
                    disciplina.descricaoDisciplina = resul["descricaoDisciplina"].ToString();
                }
                return disciplina;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from disciplina where idDisciplina = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Disciplina! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Disciplina ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

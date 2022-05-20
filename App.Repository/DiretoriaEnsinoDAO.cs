using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class DiretoriaEnsinoDAO : IGenericDAO<DiretoriaEnsinoDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(DiretoriaEnsinoDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idDiretoriaEnsino == 0)
                {
                    cmd.CommandText = "insert into diretoriaEnsino(descricaoDiretoriaEnsino, cep) values(@descricao, @cep);";
                }
                else
                {
                    cmd.CommandText = "update diretoriaEnsino set descricaoDiretoriaEnsino = @descricao, " + 
                        "cep = @cep where idDiretoriaEnsino = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idDiretoriaEnsino);
                }
                cmd.Parameters.AddWithValue("@descricao", obj.descricaoDiretoriaEnsino);
                cmd.Parameters.AddWithValue("@cep", obj.cep);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar DiretoriaEnsino! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO DiretoriaEnsino ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<DiretoriaEnsinoDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<DiretoriaEnsinoDTO> lista = new List<DiretoriaEnsinoDTO>();
            DiretoriaEnsinoDTO diretoria = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select * from DiretoriaEnsino;";
                else
                    cmd.CommandText = "select * from diretoriaEnsino where cep like '%"+busca+";";

                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    diretoria = new DiretoriaEnsinoDTO();
                    diretoria.idDiretoriaEnsino = int.Parse(resul["idDiretoriaEnsino"].ToString());
                    diretoria.descricaoDiretoriaEnsino = resul["descricaoDiretoriaEnsino"].ToString();
                    diretoria.cep = resul["cep"].ToString();

                    lista.Add(diretoria);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Diretoria de Ensino! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO DiretoriaEnsino ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public DiretoriaEnsinoDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            DiretoriaEnsinoDTO diretoria = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from DiretoriaEnsino where idDiretoriaEnsino = @id";
                cmd.Parameters.AddWithValue("@id", idObject);

                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    diretoria = new DiretoriaEnsinoDTO();

                    diretoria.idDiretoriaEnsino = int.Parse(resul["idDiretoriaEnsino"].ToString());
                    diretoria.descricaoDiretoriaEnsino = resul["descricaoDiretoriaEnsino"].ToString();
                    diretoria.cep = resul["cep"].ToString();
                }
                return diretoria;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar DiretoriaEnsino! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO DiretoriaEnsino ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from DiretoriaEnsino where idDiretoriaEnsino = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir DiretoriaEnsino! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO DiretoriaEnsino ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

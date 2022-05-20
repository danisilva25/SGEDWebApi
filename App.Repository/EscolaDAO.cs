using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class EscolaDAO : IGenericDAO<EscolaDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(EscolaDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idEscola == 0)
                {
                    cmd.CommandText = "insert into escola(nomeEscola, telefone, cep, diretoriaEnsino, idebAnosFinais, idebEnsinoMedio) "+
                    "values(@nome, @telefone, @cep, @diretoria, @idebAnosFinais, @idebEnsinoMedio);";
                }
                else
                {
                    cmd.CommandText = "update escola set nomeEscola = @nome, telefone = @telefone, cep = @cep, " + 
                        "diretoriaEnsino = @diretoria, idebAnosFinais = @idebAnosFinais, idebEnsinoMedio = @idebEnsinoMedio where idEscola = @id";
                    cmd.Parameters.AddWithValue("@id", obj.idEscola);
                }
                cmd.Parameters.AddWithValue("@nome", obj.nomeEscola);
                cmd.Parameters.AddWithValue("@telefone", obj.telefone);
                cmd.Parameters.AddWithValue("@cep", obj.cep);
                cmd.Parameters.AddWithValue("@diretoria", obj.diretoriaEnsino.idDiretoriaEnsino);
                cmd.Parameters.AddWithValue("@idebAnosFinais", obj.idebAnosFinais);
                cmd.Parameters.AddWithValue("@idebEnsinoMedio", obj.idebEnsinoMedio);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<EscolaDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<EscolaDTO> lista = new List<EscolaDTO>();
            EscolaDTO escola = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select e.*, de.cep as 'cepDiretoria', de.descricaoDiretoriaEnsino from escola e "+
                        "inner join DiretoriaEnsino de on e.diretoriaEnsino = de.idDiretoriaEnsino;";
                else
                    cmd.CommandText = "select e.*, de.cep as 'cepDiretoria', de.descricaoDiretoriaEnsino from escola e "+
                        "inner join DiretoriaEnsino de on e.diretoriaEnsino = de.idDiretoriaEnsino where p.cep like'%"+busca+";";

                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    escola = new EscolaDTO();
                    escola.idEscola = int.Parse(resul["idEscola"].ToString());
                    escola.nomeEscola = resul["nomeEscola"].ToString();
                    escola.telefone = resul["telefone"].ToString();
                    escola.cep = resul["cep"].ToString();
                    escola.diretoriaEnsino = new DiretoriaEnsinoDTO(int.Parse(resul["diretoriaEnsino"].ToString()), resul["descricaoDiretoriaEnsino"].ToString(), resul["cepDiretoria"].ToString());
                    escola.idebAnosFinais = double.Parse(resul["idebAnosFinais"].ToString());
                    escola.idebEnsinoMedio = double.Parse(resul["idebEnsinoMedio"].ToString());

                    lista.Add(escola);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public EscolaDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            EscolaDTO escola = new EscolaDTO();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select e.*, de.descricaoDiretoriaEnsino, de.cep as 'cepDiretoria' from escola e " +
                    "inner join DiretoriaEnsino de on de.idDiretoriaEnsino = e.diretoriaEnsino where e.idEscola = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    escola.idEscola = int.Parse(resul["idEscola"].ToString());
                    escola.nomeEscola = resul["nomeEscola"].ToString();
                    escola.telefone = resul["telefone"].ToString();
                    escola.cep = resul["cep"].ToString();
                    escola.diretoriaEnsino = new DiretoriaEnsinoDTO(int.Parse(resul["diretoriaEnsino"].ToString()), resul["descricaoDiretoriaEnsino"].ToString(), resul["cepDiretoria"].ToString());
                    escola.idebAnosFinais = double.Parse(resul["idebAnosFinais"].ToString());
                    escola.idebEnsinoMedio = double.Parse(resul["idebEnsinoMedio"].ToString());
                }
                return escola;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from escola where idEscola = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao escluir Escola! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Escola ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class EmailDAO : IGenericDAO<EmailDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(EmailDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idEmail == 0 && obj.escola != null)
                {
                    cmd.CommandText = "insert into email(descricaoEmail, escola) values(@descricao, @escola);";
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                }
                else if (obj.idEmail == 0 && obj.usuario != null)
                {
                    cmd.CommandText = "insert into email(descricaoEmail, usuario) values(@descricao, @usuario);";
                    cmd.Parameters.AddWithValue("@usuario", obj.usuario.idUsuario);
                }
                else
                {
                    if (obj.usuario != null)
                    {
                        cmd.CommandText = "update email set descricaoEmail = @descricao, usuario = @usuario, escola = NULL "+
                        "where idEmail = @id;";
                        cmd.Parameters.AddWithValue("@usuario", obj.usuario.idUsuario);
                    }
                    else
                    {
                        cmd.CommandText = "update email set descricaoEmail = @descricao, escola = @escola, usuario = NULL " +
                        "where idEmail = @id;";
                        cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    }
                    cmd.Parameters.AddWithValue("@id", obj.idEmail);
                }
                cmd.Parameters.AddWithValue("@descricao", obj.descricaoEmail);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar email! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Email ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<EmailDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<EmailDTO> lista = new List<EmailDTO>();
            EmailDTO email = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select e.*, es.nomeEscola, u.nomeUsuario from email e left join escola es on es.idEscola = e.escola " +
                "left join usuario u on u.idUsuario = e.usuario;";
                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    email = new EmailDTO();
                    email.idEmail = Convert.ToInt32(resul["idEmail"]);
                    email.descricaoEmail = resul["descricaoEmail"].ToString();
                    email.escola = new EscolaDTO(0, resul["nomeEscola"].ToString());
                    email.usuario = new UsuarioDTO(0, resul["nomeUsuario"].ToString());
                    lista.Add(email);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Email! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Email ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public EmailDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            EmailDTO email = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from email where idEmail = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    email = new EmailDTO();
                    email.idEmail = Convert.ToInt32(resul["idEmail"]);
                    email.descricaoEmail = resul["descricaoEmail"].ToString();
                    if (!string.IsNullOrEmpty(resul["escola"].ToString()))
                        email.escola = new EscolaDTO(Convert.ToInt32(resul["escola"]), null);
                    else if(!string.IsNullOrEmpty(resul["usuario"].ToString()))
                        email.usuario = new UsuarioDTO(Convert.ToInt32(resul["usuario"]), null);
                }
                return email;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Email! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Email ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from email where idEmail = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir email! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Email ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

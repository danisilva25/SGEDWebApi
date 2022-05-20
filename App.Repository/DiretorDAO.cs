using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class DiretorDAO : IGenericDAO<DiretorDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(DiretorDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idUsuario == 0)
                {                   
                    cmd.CommandText = "insert into diretor(escola, usuario) values(@escola, @usuario);";
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    cmd.Parameters.AddWithValue("@usuario", new UsuarioDAO().gravarOutrosUsuarios(obj, 1));
                    cmd.ExecuteNonQuery();
                }
                else if (string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    new UsuarioDAO().gravarOutrosUsuarios(obj, null);
                    cmd.CommandText = "update diretor set escola = @escola where usuario = @idUser;";
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    cmd.Parameters.AddWithValue("@idUser", obj.idUsuario);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    new UsuarioDAO().gravarOutrosUsuarios(obj, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar Diretor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Diretor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<DiretorDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<DiretorDTO> lista = new List<DiretorDTO>();
            DiretorDTO diretor = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select u.nomeUsuario, u.idade, d.*, e.* from diretor d " +
                        "inner join usuario u on d.usuario = u.idUsuario " +
                        "inner join escola e on e.idEscola = d.escola where u.nomeUsuario like '%" + busca + ";";
                else
                    cmd.CommandText = "select u.nomeUsuario, u.idade, d.*, e.* from diretor d " + 
                        "inner join usuario u on d.usuario = u.idUsuario " +
                        "inner join escola e on e.idEscola = d.escola;";

                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    diretor = new DiretorDTO();

                    diretor.idUsuario = int.Parse(resul["usuario"].ToString());
                    diretor.nomeUsuario = resul["nomeUsuario"].ToString();
                    diretor.idade = Int16.Parse(resul["idade"].ToString());
                    diretor.escola = new EscolaDTO(int.Parse(resul["idEscola"].ToString()), resul["nomeEscola"].ToString());

                    lista.Add(diretor);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Diretor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Diretor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public DiretorDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            DiretorDTO diretor = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select d.*, u.* from diretor d " +
                "inner join usuario u on d.usuario = u.idUsuario where d.usuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    diretor = new DiretorDTO();
                    diretor.idUsuario = int.Parse(resul["idUsuario"].ToString());
                    diretor.nomeUsuario = resul["nomeUsuario"].ToString();
                    diretor.idade = Int16.Parse(resul["idade"].ToString());
                    diretor.loginUsuario = resul["loginUsuario"].ToString();
                    diretor.escola = new EscolaDTO(int.Parse(resul["escola"].ToString()), null);
                }
                return diretor;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Diretor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Carregar ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from diretor where usuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from usuario where idUsuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Diretor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Diretor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

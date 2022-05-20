using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace SGED.Repository
{
    public class UsuarioDAO : IGenericDAO<UsuarioDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(UsuarioDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            try
            {
                String salt = GenarateSalt();
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idUsuario == 0)
                {
                    cmd.CommandText = "insert into usuario(nomeUsuario, idade, salt, loginUsuario, senhaUsuario, tipoUsuario) " + 
                        "values(@nome, @idade, @salt, @login, @senha, @tipo);";   
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                    cmd.Parameters.AddWithValue("@tipo", obj.tipoUsuario);
                }
                else if(!string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    cmd.CommandText = "update usuario set nomeUsuario = @nome, idade = @idade, " + 
                        "loginUsuario = @login, senhaUsuario = @senha where idusuario = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                    cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                    cmd.Parameters.AddWithValue("@salt", salt);
                }
                else
                {
                    cmd.CommandText = "update usuario set nomeUsuario = @nome, idade = @idade, " +
                        "loginUsuario = @login where idusuario = @id;";
                    cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                }       
                cmd.Parameters.AddWithValue("@nome", obj.nomeUsuario);
                cmd.Parameters.AddWithValue("@idade", obj.idade);
                cmd.Parameters.AddWithValue("@login", obj.loginUsuario);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar usuário! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Usuario ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<UsuarioDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<UsuarioDTO> lista = new List<UsuarioDTO>();
            UsuarioDTO usuario = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select * from usuario;";
                else
                    cmd.CommandText = "select * from usuario where nomeUsuario like '%" + busca + ";";

                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    usuario = new UsuarioDTO();

                    usuario.idUsuario = int.Parse(resul["idUsuario"].ToString());
                    usuario.nomeUsuario = resul["nomeUsuario"].ToString();
                    usuario.idade = Convert.ToInt16(resul["idade"]);

                    lista.Add(usuario);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Usuario! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Usuario ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public UsuarioDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            UsuarioDTO usuario = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from usuario where idUsuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    usuario = new UsuarioDTO();

                    usuario.idUsuario = int.Parse(resul["idUsuario"].ToString());
                    usuario.nomeUsuario = resul["nomeUsuario"].ToString();
                    usuario.idade = Convert.ToInt16(resul["idade"]);
                    usuario.loginUsuario = resul["loginUsuario"].ToString();
                }
                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Usuario! Erro: " + ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Erro na DAO Usuario ao fechar os parâmetros de conexão! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from usuario where idUsuario = @id;";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Usuario! Erro: " + ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Erro na DAO Usuario ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        private String GenarateSalt()
        {
            var saltBytes = new Byte[80];

            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        private String HashPassword(String salt, String senha)
        {
            int iteration = 1000;
            int nHash = 80;

            var saltBytes = Convert.FromBase64String(salt);

            using(var rfc289DeriveBytes = new Rfc2898DeriveBytes(senha, saltBytes, iteration))
            {
                return Convert.ToBase64String(rfc289DeriveBytes.GetBytes(nHash));
            }
        }

        public Int32? gravarOutrosUsuarios(UsuarioDTO obj, int? tipo)
        {
            Int32? resul = null;
            SqlConnection conn = new SqlConnection(stringdeconexao);
            try
            {
                String salt = GenarateSalt();
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idUsuario == 0)
                {
                    cmd.CommandText = "insert into usuario(nomeUsuario, idade, salt, loginUsuario, senhaUsuario, tipoUsuario) " +
                        "values(@nome, @idade, @salt, @login, @senha, @tipo); select SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@nome", obj.nomeUsuario);
                    cmd.Parameters.AddWithValue("@idade", obj.idade);
                    cmd.Parameters.AddWithValue("@login", obj.loginUsuario);
                    resul = Convert.ToInt32(cmd.ExecuteScalar());
                    return resul;
                }
                else if (string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    cmd.CommandText = "update usuario set nomeUsuario = @nome, idade = @idade, loginUsuario = @login " +
                        "where idUsuario = @id";
                    cmd.Parameters.AddWithValue("@nome", obj.nomeUsuario);
                    cmd.Parameters.AddWithValue("@idade", obj.idade);
                    cmd.Parameters.AddWithValue("@login", obj.loginUsuario);
                    cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                    cmd.ExecuteNonQuery();
                    return null;
                }
                else
                {
                    cmd.CommandText = "update usuario set senhaUsuario = @senha, salt = @salt where idUsuario = @id";
                    cmd.Parameters.AddWithValue("@senha", HashPassword(salt, obj.senhaUsuario));
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Parameters.AddWithValue("@id", obj.idUsuario);
                    cmd.ExecuteNonQuery();
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao salvar usuário! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Usuario ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
        public UsuarioDTO logar(String login, String senha)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            UsuarioDTO usuario = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from usuario where loginUsuario = @login;";
                cmd.Parameters.AddWithValue("@login", login);
                var user = cmd.ExecuteReader();

                if (!user.Read())
                {
                    return null;
                }
                else
                {
                    var salt = user["salt"].ToString();
                    var senhaBanco = user["senhaUsuario"].ToString();

                    if (HashPassword(salt, senha) != user["senhausuario"].ToString())
                        return null;
                    else
                    {
                        usuario = new UsuarioDTO();
                        usuario.nomeUsuario = user["nomeUsuario"].ToString();
                        usuario.tipoUsuario = Convert.ToInt16(user["tipoUsuario"]);
                        return usuario;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na DAO Usuario ao logar no sistema! Erro: " + ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    conn.Dispose();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Erro na DAO Usuario ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class AlunoDAO : IGenericDAO<AlunoDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(AlunoDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idUsuario == 0)
                {
                    cmd.CommandText = "insert into aluno(ra, turma, escola, usuario) values (@ra, @turma, " +
                    "@escola, @usuario)";
                    cmd.Parameters.AddWithValue("@ra", obj.ra);
                    cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    cmd.Parameters.AddWithValue("@usuario", new UsuarioDAO().gravarOutrosUsuarios(obj,3));
                    cmd.ExecuteNonQuery();
                }
                else if (string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    new UsuarioDAO().gravarOutrosUsuarios(obj, null);
                    cmd.CommandText = "update aluno set ra = @ra, turma = @turma, escola = @escola where usuario = @id";
                    cmd.Parameters.AddWithValue("@ra", obj.ra);
                    cmd.Parameters.AddWithValue("@turma", obj.turma.idTurma);
                    cmd.Parameters.AddWithValue("@escola", obj.escola.idEscola);
                    cmd.Parameters.AddWithValue("@id", obj.idUsuario);
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
                Console.WriteLine("Erro na DAO ao salvar Aluno! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Aluno ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<AlunoDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<AlunoDTO> lista = new List<AlunoDTO>();
            AlunoDTO aluno = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select a.idAluno, u.nomeUsuario, e.nomeEscola, t.descricaoTurma from aluno a " + 
                    "inner join escola e on e.idEscola = a.escola inner join usuario u on u.idUsuario = a.usuario " + 
                    "inner join turma t on t.idTurma = a.turma";
                else
                    cmd.CommandText = "select a.idAluno, u.nomeUsuario, e.nomeEscola, t.descricaoTurma from aluno a " +
                    "inner join escola e on e.idEscola = a.escola inner join usuario u on u.idUsuario = a.usuario " +
                    "inner join turma t on t.idTurma = a.turma where u.nomeUsuario like '%"+busca;

                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    aluno = new AlunoDTO();
                    aluno.idUsuario = Convert.ToInt32(resul["idAluno"]);
                    aluno.nomeUsuario = resul["nomeUsuario"].ToString();
                    aluno.escola = new EscolaDTO(0, resul["nomeEscola"].ToString());
                    aluno.turma = new TurmaDTO(0, resul["descricaoTurma"].ToString());
                    lista.Add(aluno);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Aluno! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Aluno ao fechar os parâmetros de conexao! Erro: " + ex.Message);
                }
            }
        }

        public AlunoDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            AlunoDTO aluno = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select a.*, u.*, t.idTurma, e.idEscola from aluno a "+
                "inner join usuario u on u.idUsuario = a.usuario inner join turma t on t.idTurma = a.turma " +
                "inner join escola e on e.idEscola = a.escola where a.usuario = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    aluno = new AlunoDTO();
                    aluno.idUsuario = Convert.ToInt32(resul["idUsuario"]);
                    aluno.nomeUsuario = resul["nomeUsuario"].ToString();
                    aluno.idade = Int16.Parse(resul["idade"].ToString());
                    aluno.loginUsuario = resul["loginUsuario"].ToString();
                    aluno.ra = resul["ra"].ToString();
                    aluno.turma = new TurmaDTO(Convert.ToInt32(resul["turma"]), null);
                    aluno.escola = new EscolaDTO(Convert.ToInt32(resul["escola"]), null);
                }
                return aluno;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Aluno! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from aluno where usuario = @id;";
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
                Console.WriteLine("Erro na DAO ao excluir Aluno! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Aluno ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

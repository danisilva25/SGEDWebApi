using SGED.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SGED.Repository
{
    public class ProfessorDAO : IGenericDAO<ProfessorDTO>
    {
        String stringdeconexao = ConfigurationManager.ConnectionStrings["StringDeConexao"].ConnectionString;

        public bool salvar(ProfessorDTO obj)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (obj.idUsuario == 0)
                {                    
                    cmd.CommandText = "insert into professor(cpf, rg, matricula, categoria, " + 
                    "horaaula, disponivel, diretoriaEnsino, usuario) values (@cpf, @rg, @matricula, " +
                    "@categoria, @hora, @disponivel, @diretoria, @usuario)";
                    cmd.Parameters.AddWithValue("@cpf", obj.cpf);
                    cmd.Parameters.AddWithValue("@rg", obj.rg);
                    cmd.Parameters.AddWithValue("@matricula", obj.matricula);
                    cmd.Parameters.AddWithValue("@categoria", obj.categoria);
                    cmd.Parameters.AddWithValue("@hora", obj.horaAula);
                    cmd.Parameters.AddWithValue("@disponivel", obj.disponivel);
                    cmd.Parameters.AddWithValue("@diretoria", obj.diretoriaEnsino.idDiretoriaEnsino);
                    cmd.Parameters.AddWithValue("@usuario", new UsuarioDAO().gravarOutrosUsuarios(obj,2));
                    cmd.ExecuteNonQuery();
                }
                else if (string.IsNullOrEmpty(obj.senhaUsuario))
                {
                    new UsuarioDAO().gravarOutrosUsuarios(obj, null);
                    cmd.CommandText = "update professor set cpf = @cpf, rg = @rg, matricula = @matricula, categoria = @categoria, " +
                    "horaaula = @hora, disponivel = @disponivel, diretoriaEnsino = @diretoria where usuario = @id";
                    cmd.Parameters.AddWithValue("@cpf", obj.cpf);
                    cmd.Parameters.AddWithValue("@rg", obj.rg);
                    cmd.Parameters.AddWithValue("@matricula", obj.matricula);
                    cmd.Parameters.AddWithValue("@categoria", obj.categoria);
                    cmd.Parameters.AddWithValue("@hora", obj.horaAula);
                    cmd.Parameters.AddWithValue("@disponivel", obj.disponivel);
                    cmd.Parameters.AddWithValue("@diretoria", obj.diretoriaEnsino.idDiretoriaEnsino);
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
                Console.WriteLine("Erro na DAO ao salvar Professor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public List<ProfessorDTO> listar(string busca)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            List<ProfessorDTO> lista = new List<ProfessorDTO>();
            ProfessorDTO professor = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(busca))
                    cmd.CommandText = "select u.idusuario, u.nomeUsuario, d.descricaoDiretoriaEnsino, p.idprofessor, p.categoria, p.horaaula from professor p "+
                    "inner join usuario u on u.idUsuario = p.usuario "+
                    "inner join diretoriaEnsino d on d.idDiretoriaEnsino = p.diretoriaEnsino";
                else
                    cmd.CommandText = "select u.idusuario, u.nomeUsuario, d.descricaoDiretoriaEnsino, p.idprofessor, p.categoria, p.horaaula from professor p " +
                    "inner join usuario u on u.idUsuario = p.usuario " +
                    "inner join diretoriaEnsino d on d.idDiretoriaEnsino = p.diretoriaEnsino " + 
                    "where u.nomeUsuario like '%" + busca + ";";

                var resul = cmd.ExecuteReader();

                while (resul.Read())
                {
                    professor = new ProfessorDTO();
                    professor.idUsuario = int.Parse(resul["idProfessor"].ToString());
                    professor.nomeUsuario = resul["nomeUsuario"].ToString();
                    professor.horaAula = Convert.ToInt16(resul["horaaula"].ToString());
                    professor.categoria = Convert.ToChar(resul["categoria"].ToString());
                    professor.diretoriaEnsino = new DiretoriaEnsinoDTO(0, resul["descricaoDiretoriaEnsino"].ToString(), null);
                    
                    lista.Add(professor);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao listar Professor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Professor ao fechar os parâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }

        public ProfessorDTO carregar(int idObject)
        {
            SqlConnection conn = new SqlConnection(stringdeconexao);
            ProfessorDTO professor = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select p.*, d.*, u.* from professor p inner join "+
                "diretoriaEnsino d on d.idDiretoriaEnsino = p.diretoriaEnsino "+
                "inner join usuario u on u.idusuario = p.usuario where u.idUsuario = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                var resul = cmd.ExecuteReader();

                if (resul.Read())
                {
                    professor = new ProfessorDTO();
                    professor.idUsuario = Convert.ToInt32(resul["idusuario"]);
                    professor.nomeUsuario = resul["nomeUsuario"].ToString();
                    professor.idade = Convert.ToInt16(resul["idade"]);
                    professor.loginUsuario = resul["loginUsuario"].ToString();
                    professor.matricula = resul["matricula"].ToString();
                    professor.rg = resul["rg"].ToString();
                    professor.cpf = resul["cpf"].ToString();
                    professor.categoria = Char.Parse(resul["categoria"].ToString());
                    professor.diretoriaEnsino = new DiretoriaEnsinoDTO(Convert.ToInt32(resul["diretoriaEnsino"]), resul["descricaoDiretoriaEnsino"].ToString(), null);
                    professor.horaAula = Convert.ToInt16(resul["horaAula"]);
                    professor.disponivel = Convert.ToBoolean(resul["disponivel"]);
                }
                return professor;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao carregar Professor! Erro: " + ex.Message);
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
                cmd.CommandText = "delete from professor where usuario = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from usuario where idUsuario = @id";
                cmd.Parameters.AddWithValue("@id", idObject);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na DAO ao excluir Professor! Erro: " + ex.Message);
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
                    Console.WriteLine("Erro na DAO Excluir ao fechar os paâmetros de conexão! Erro: " + ex.Message);
                }
            }
        }
    }
}

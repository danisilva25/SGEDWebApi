using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Usuario
    {
        public Boolean salvar(UsuarioDTO usuario)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return dao.salvar(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Usuário! Erro: " + ex.Message);
            }
        }

        public List<UsuarioDTO> listar(string busca)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Usuário! Erro: " + ex.Message);
            }
        }

        public UsuarioDTO carregar(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Usuário! Erro: " + ex.Message);
            }
        }

        public Boolean excluir(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Usuário! Erro: " + ex.Message);
            }
        }

        public UsuarioDTO logar(String login, String senha)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                return dao.logar(login, senha);
            }
            catch(Exception ex)
            {
                throw new Exception("Erro na Model ao logar! Erro: " + ex.Message);
            }
        }
    }
}
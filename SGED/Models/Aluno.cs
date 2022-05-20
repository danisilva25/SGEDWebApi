using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Aluno
    {
        public Boolean salvar(AlunoDTO aluno)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return dao.salvar(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Aluno! Erro: " + ex.Message);
            }
        }
        public List<AlunoDTO> listar(string busca)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Aluno! Erro: " + ex.Message);
            }
        }
        public AlunoDTO carregar(int id)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Aluno! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                AlunoDAO dao = new AlunoDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Aluno! Erro: " + ex.Message);
            }
        }
    }
}
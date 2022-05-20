using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SGED.Domain;
using SGED.Repository;

namespace SGED.Models
{
    public class Turma_Professor
    {
        public Boolean salvar(Turma_ProfessorDTO turma_professor)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return dao.salvar(turma_professor);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma_Professor! Erro: " + ex.Message);
            }
        }
        public List<Turma_ProfessorDTO> listar(String busca)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Turma_Professor! Erro: " + ex.Message);
            }
        }
        public Turma_ProfessorDTO carregar(int id)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Turma_Professor! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                Turma_ProfessorDAO dao = new Turma_ProfessorDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Turma_Professor! Erro: " + ex.Message);
            }
        }
    }
}
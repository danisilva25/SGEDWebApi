using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Professor
    {
        public Boolean salvar(ProfessorDTO professor)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return dao.salvar(professor);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar professor! Erro: " + ex.Message);
            }
        }
        public List<ProfessorDTO> listar(string busca)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Professor! Erro: " + ex.Message);
            }
        }
        public ProfessorDTO carregar(int id)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Professor! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                ProfessorDAO dao = new ProfessorDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Professor! Erro: " + ex.Message);
            }
        }
    }
}
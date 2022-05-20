using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Disciplina
    {
        public Boolean salvar(DisciplinaDTO disciplina)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return dao.salvar(disciplina);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Disciplina! Erro: " + ex.Message);
            }
        }
        public List<DisciplinaDTO> listar(String buscar)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return dao.listar(buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Disciplina! Erro: " + ex.Message);
            }
        }
        public DisciplinaDTO carregar(int id)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Disciplina! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                DisciplinaDAO dao = new DisciplinaDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Disciplina! Erro: " + ex.Message);
            }
        }
    }
}
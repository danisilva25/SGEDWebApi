using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Turma_Disciplina
    {
        public Boolean salvar(Turma_DisciplinaDTO turma_disciplina)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return dao.salvar(turma_disciplina);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma_Disciplina! Erro: " + ex.Message);
            }
        }
        public List<Turma_DisciplinaDTO> listar(String busca)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Turma_Disciplina! Erro: " + ex.Message);
            }
        }
        public Turma_DisciplinaDTO carregar(int id)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Turma_Disciplina! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                Turma_DisciplinaDAO dao = new Turma_DisciplinaDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Turma_Disciplina! Erro: " + ex.Message);
            }
        }
    }
}
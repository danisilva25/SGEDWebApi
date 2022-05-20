using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Professor_Disciplina
    {
        public Boolean salvar(Professor_DisciplinaDTO professor_disciplina)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return dao.salvar(professor_disciplina);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Professor_Disciplina! Erro: " + ex.Message);
            }
        }
        public List<Professor_DisciplinaDTO> listar(String busca)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Professor_Disciplina! Erro: " + ex.Message);
            }
        }
        public Professor_DisciplinaDTO carregar(int id)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Professor_Disciplina! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                Professor_DisciplinaDAO dao = new Professor_DisciplinaDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Profsssor_Disciplina! Erro: " + ex.Message);
            }
        }
    }
}
using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Turma
    {
        public Boolean salvar(TurmaDTO turma)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return dao.salvar(turma);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma! Erro: " + ex.Message);
            }
        }

        public List<TurmaDTO> listar(String busca)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Turma! Erro: " + ex.Message);
            }
        }

        public TurmaDTO carregar(int id)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Turma! Erro: " + ex.Message);
            }
        }

        public Boolean excluir(int id)
        {
            try
            {
                TurmaDAO dao = new TurmaDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Turma! Erro: " + ex.Message);
            }
        }
    }
}
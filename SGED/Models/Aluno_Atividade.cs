using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Aluno_Atividade
    {
        public Boolean salvar(Aluno_AtividadeDTO aluno_atividade)
        {
            try
            {
                Aluno_AtividadeDAO dao = new Aluno_AtividadeDAO();
                return dao.salvar(aluno_atividade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar Aluno_Atividade! Erro: " + ex.Message);
            }
        }
        public List<Aluno_AtividadeDTO> listar(String busca)
        {
            try
            {
                Aluno_AtividadeDAO dao = new Aluno_AtividadeDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Aluno_Atividade! Erro: " + ex.Message);
            }
        }
        public Aluno_AtividadeDTO carregar(int id)
        {
            try
            {
                Aluno_AtividadeDAO dao = new Aluno_AtividadeDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Aluno_Atividade! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                Aluno_AtividadeDAO dao = new Aluno_AtividadeDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Aluno_Atividade! Erro: " + ex.Message);
            }
        }
    }
}
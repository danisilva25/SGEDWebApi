using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Atividade
    {
        public Boolean salvar(AtividadeDTO atividade)
        {
            try
            {
                AtividadeDAO dao = new AtividadeDAO();
                return dao.salvar(atividade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar Atividade! Erro: " + ex.Message);
            }
        }
        public List<AtividadeDTO> listar(String busca)
        {
            try
            {
                AtividadeDAO dao = new AtividadeDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Atividade! Erro: " + ex.Message);
            }
        }
        public AtividadeDTO carregar(int id)
        {
            try
            {
                AtividadeDAO dao = new AtividadeDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Atividade! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                AtividadeDAO dao = new AtividadeDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Atividade! Erro: " + ex.Message);
            }
        }
    }
}
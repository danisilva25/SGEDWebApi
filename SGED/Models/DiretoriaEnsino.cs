using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class DiretoriaEnsino
    {
        public Boolean salvar(DiretoriaEnsinoDTO diretoria)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return dao.salvar(diretoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar DiretoriaEnsino! Erro: " + ex.Message);
            }
        }
        public List<DiretoriaEnsinoDTO> listar(string busca)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar DiretoriaEnsino! Erro: " + ex.Message);
            }
        }
        public DiretoriaEnsinoDTO carregar(int id)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar DiretoriaEnsino! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                DiretoriaEnsinoDAO dao = new DiretoriaEnsinoDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir DiretoriaEnsino! Erro: " + ex.Message);
            }
        }
    }
}
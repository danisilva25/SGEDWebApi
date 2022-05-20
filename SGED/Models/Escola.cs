using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Escola
    {
        public Boolean salvar(EscolaDTO escola)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return dao.salvar(escola);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar Escola! Escola! " + ex.Message);
            }
        }
        public List<EscolaDTO> listar(String busca)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Escola! Erro: " + ex.Message);
            }
        }
        public EscolaDTO carregar(int id)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Escola! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                EscolaDAO dao = new EscolaDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Escola! Erro: " + ex.Message);
            }
        }
    }
}
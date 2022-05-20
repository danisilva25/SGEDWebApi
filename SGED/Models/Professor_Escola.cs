using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Professor_Escola
    {
        public Boolean salvar(Professor_EscolaDTO professor_escola)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return dao.salvar(professor_escola);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Professor_Escola! Erro: " + ex.Message);
            }
        }
        public List<Professor_EscolaDTO> listar(String busca)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Professor_Escola! Erro: " + ex.Message);
            }
        }
        public Professor_EscolaDTO carregar(int id)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Professor_Escola! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                Professor_EscolaDAO dao = new Professor_EscolaDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Professor_Escola! Erro: " + ex.Message);
            }
        }
    }
}
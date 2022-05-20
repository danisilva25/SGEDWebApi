using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Diretor
    {
        public Boolean salvar(DiretorDTO diretor)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return dao.salvar(diretor);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar Diretor! Erro: " + ex.Message);
            }
        }
        public List<DiretorDTO> listar(string busca)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Diretor! Erro: " + ex.Message);
            }
        }
        public DiretorDTO carregar(int id)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Diretor! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                DiretorDAO dao = new DiretorDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Diretor! Erro: " + ex.Message);
            }
        }   
    }
}
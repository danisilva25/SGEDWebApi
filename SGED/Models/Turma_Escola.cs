using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Turma_Escola
    {
        public Boolean salvar(Turma_EscolaDTO turma_escola)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return dao.salvar(turma_escola);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao salvar Turma_Escola! Erro: " + ex.Message);
            }
        }
        public List<Turma_EscolaDTO> listar(String busca)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao listar Turma_Escola! Erro: " + ex.Message);
            }
        }
        public Turma_EscolaDTO carregar(int id)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao carregar Turma_Escola! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                Turma_EscolaDAO dao = new Turma_EscolaDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Model ao excluir Turma_Escola! Erro: " + ex.Message);
            }
        }
    }
}
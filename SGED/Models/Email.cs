using SGED.Domain;
using SGED.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGED.Models
{
    public class Email
    {
        public Boolean salvar(EmailDTO email)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return dao.salvar(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao salvar Email! Erro: " + ex.Message);
            }
        }
        public List<EmailDTO> listar(String busca)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return dao.listar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao listar Email! Erro: " + ex.Message);
            }
        }
        public EmailDTO carregar(int id)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return dao.carregar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao carregar Email! Erro: " + ex.Message);
            }
        }
        public Boolean excluir(int id)
        {
            try
            {
                EmailDAO dao = new EmailDAO();
                return dao.excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na model ao excluir Email! Erro: " + ex.Message);
            }
        }
    }
}
using SGED.Domain;
using SGED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SGED.Controllers
{
    public class EmailController : ApiController
    {
        // GET: api/Email
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<EmailDTO> lista = new Email().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Email/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Email().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Email
        [HttpPost]
        public Boolean Salvar(EmailDTO Email)
        {
            try
            {
                return new Email().salvar(Email);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Email! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Email/5
        [HttpPut]
        public Boolean Alterar(EmailDTO Email)
        {
            try
            {
                return new Email().salvar(Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Email! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Email/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Email().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Email! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

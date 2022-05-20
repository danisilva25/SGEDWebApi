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
    public class AtividadeController : ApiController
    {
        // GET: api/Atividade
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<AtividadeDTO> lista = new Atividade().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Atividade/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Atividade().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Atividade
        [HttpPost]
        public Boolean Salvar(AtividadeDTO Atividade)
        {
            try
            {
                return new Atividade().salvar(Atividade);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Atividade/5
        [HttpPut]
        public Boolean Alterar(AtividadeDTO Atividade)
        {
            try
            {
                return new Atividade().salvar(Atividade);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Atividade/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Atividade().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Atividade! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

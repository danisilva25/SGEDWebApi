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
    public class EscolaController : ApiController
    {
        // GET: api/Escola
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<EscolaDTO> lista = new Escola().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Escola/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Escola().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Escola
        [HttpPost]
        public Boolean Salvar(EscolaDTO Escola)
        {
            try
            {
                return new Escola().salvar(Escola);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Escola/5
        [HttpPut]
        public Boolean Alterar(EscolaDTO Escola)
        {
            try
            {
                return new Escola().salvar(Escola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Escola/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Escola().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Escola! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

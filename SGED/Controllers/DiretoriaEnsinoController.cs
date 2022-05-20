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
    public class DiretoriaEnsinoController : ApiController
    {
        // GET: api/DiretoriaEnsino
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<DiretoriaEnsinoDTO> lista = new DiretoriaEnsino().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/DiretoriaEnsino/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new DiretoriaEnsino().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/DiretoriaEnsino
        [HttpPost]
        public Boolean Salvar(DiretoriaEnsinoDTO DiretoriaEnsino)
        {
            try
            {
                return new DiretoriaEnsino().salvar(DiretoriaEnsino);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/DiretoriaEnsino/5
        [HttpPut]
        public Boolean Alterar(DiretoriaEnsinoDTO DiretoriaEnsino)
        {
            try
            {
                return new DiretoriaEnsino().salvar(DiretoriaEnsino);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/DiretoriaEnsino/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new DiretoriaEnsino().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir DiretoriaEnsino! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

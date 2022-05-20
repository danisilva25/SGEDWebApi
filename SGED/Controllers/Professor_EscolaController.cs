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
    public class Professor_EscolaController : ApiController
    {
        // GET: api/Professor_Escola
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<Professor_EscolaDTO> lista = new Professor_Escola().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Professor_Escola/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Professor_Escola().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Professor_Escola
        [HttpPost]
        public Boolean Salvar(Professor_EscolaDTO Professor_Escola)
        {
            try
            {
                return new Professor_Escola().salvar(Professor_Escola);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Professor_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Professor_Escola/5
        [HttpPut]
        public Boolean Alterar(Professor_EscolaDTO Professor_Escola)
        {
            try
            {
                return new Professor_Escola().salvar(Professor_Escola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Professor_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Professor_Escola/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Professor_Escola().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Professor_Escola! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

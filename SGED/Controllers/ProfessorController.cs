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
    public class ProfessorController : ApiController
    {
        // GET: api/Professor
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<ProfessorDTO> lista = new Professor().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Professor/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Professor().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Professor
        [HttpPost]
        public Boolean Salvar(ProfessorDTO Professor)
        {
            try
            {
                return new Professor().salvar(Professor);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Professor/5
        [HttpPut]
        public Boolean Alterar(ProfessorDTO Professor)
        {
            try
            {
                return new Professor().salvar(Professor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Professor/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Professor().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Professor! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

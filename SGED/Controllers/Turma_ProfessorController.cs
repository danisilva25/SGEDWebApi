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
    public class Turma_ProfessorController : ApiController
    {
        // GET: api/Turma_Professor
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<Turma_ProfessorDTO> lista = new Turma_Professor().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Turma_Professor/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Turma_Professor().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Turma_Professor
        [HttpPost]
        public Boolean Salvar(Turma_ProfessorDTO Turma_Professor)
        {
            try
            {
                return new Turma_Professor().salvar(Turma_Professor);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma_Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma_Professor/5
        [HttpPut]
        public Boolean Alterar(Turma_ProfessorDTO Turma_Professor)
        {
            try
            {
                return new Turma_Professor().salvar(Turma_Professor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma_Professor! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma_Professor/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Turma_Professor().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma_Professor! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

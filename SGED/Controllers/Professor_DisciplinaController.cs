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
    public class Professor_DisciplinaController : ApiController
    {
        // GET: api/Professor_Disciplina
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<Professor_DisciplinaDTO> lista = new Professor_Disciplina().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Professor_Disciplina/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Professor_Disciplina().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Professor_Disciplina
        [HttpPost]
        public Boolean Salvar(Professor_DisciplinaDTO Professor_Disciplina)
        {
            try
            {
                return new Professor_Disciplina().salvar(Professor_Disciplina);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Professor_Disciplina/5
        [HttpPut]
        public Boolean Alterar(Professor_DisciplinaDTO Professor_Disciplina)
        {
            try
            {
                return new Professor_Disciplina().salvar(Professor_Disciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Professor_Disciplina/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Professor_Disciplina().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Professor_Disciplina! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

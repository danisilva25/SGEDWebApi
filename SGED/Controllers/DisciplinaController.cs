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
    public class DisciplinaController : ApiController
    {
        // GET: api/Disciplina
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<DisciplinaDTO> lista = new Disciplina().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Disciplina/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Disciplina().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Disciplina
        [HttpPost]
        public Boolean Salvar(DisciplinaDTO disciplina)
        {
            try
            {
                return new Disciplina().salvar(disciplina);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Disciplina/5
        [HttpPut]
        public Boolean Alterar(DisciplinaDTO disciplina)
        {
            try
            {
                return new Disciplina().salvar(disciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Disciplina/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Disciplina().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Disciplina! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

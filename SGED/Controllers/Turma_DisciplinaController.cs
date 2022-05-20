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
    public class Turma_DisciplinaController : ApiController
    {
        // GET: api/Turma_Disciplina
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<Turma_DisciplinaDTO> lista = new Turma_Disciplina().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Turma_Disciplina/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Turma_Disciplina().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Turma_Disciplina
        [HttpPost]
        public Boolean Salvar(Turma_DisciplinaDTO Turma_Disciplina)
        {
            try
            {
                return new Turma_Disciplina().salvar(Turma_Disciplina);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma_Disciplina/5
        [HttpPut]
        public Boolean Alterar(Turma_DisciplinaDTO Turma_Disciplina)
        {
            try
            {
                return new Turma_Disciplina().salvar(Turma_Disciplina);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma_Disciplina/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Turma_Disciplina().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma_Disciplina! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

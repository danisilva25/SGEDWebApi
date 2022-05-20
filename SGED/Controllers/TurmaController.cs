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
    public class TurmaController : ApiController
    {
        // GET: api/Turma
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<TurmaDTO> lista = new Turma().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Turma/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Turma().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Turma
        [HttpPost]
        public Boolean Salvar(TurmaDTO Turma)
        {
            try
            {
                return new Turma().salvar(Turma);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma/5
        [HttpPut]
        public Boolean Alterar(TurmaDTO Turma)
        {
            try
            {
                return new Turma().salvar(Turma);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Turma().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

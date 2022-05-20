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
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<AlunoDTO> lista = new Aluno().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Aluno/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Aluno().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Aluno
        [HttpPost]
        public Boolean Salvar(AlunoDTO Aluno)
        {
            try
            {
                return new Aluno().salvar(Aluno);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Aluno! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Aluno/5
        [HttpPut]
        public Boolean Alterar(AlunoDTO Aluno)
        {
            try
            {
                return new Aluno().salvar(Aluno);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Aluno! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Aluno/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Aluno().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Aluno! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

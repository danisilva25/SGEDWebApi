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
    public class Turma_EscolaController : ApiController
    {
        // GET: api/Turma_Escola
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<Turma_EscolaDTO> lista = new Turma_Escola().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Turma_Escola/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Turma_Escola().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Turma_Escola
        [HttpPost]
        public Boolean Salvar(Turma_EscolaDTO Turma_Escola)
        {
            try
            {
                return new Turma_Escola().salvar(Turma_Escola);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Turma_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Turma_Escola/5
        [HttpPut]
        public Boolean Alterar(Turma_EscolaDTO Turma_Escola)
        {
            try
            {
                return new Turma_Escola().salvar(Turma_Escola);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Turma_Escola! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Turma_Escola/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Turma_Escola().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Turma_Escola! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

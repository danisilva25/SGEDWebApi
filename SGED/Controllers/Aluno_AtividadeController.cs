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
    public class Aluno_AtividadeController : ApiController
    {
        // GET: api/Aluno_Atividade
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<Aluno_AtividadeDTO> lista = new Aluno_Atividade().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Aluno_Atividade/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Aluno_Atividade().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Aluno_Atividade
        [HttpPost]
        public Boolean Salvar(Aluno_AtividadeDTO Aluno_Atividade)
        {
            try
            {
                return new Aluno_Atividade().salvar(Aluno_Atividade);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Aluno_Atividade/5
        [HttpPut]
        public Boolean Alterar(Aluno_AtividadeDTO Aluno_Atividade)
        {
            try
            {
                return new Aluno_Atividade().salvar(Aluno_Atividade);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Aluno_Atividade/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Aluno_Atividade().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Aluno_Atividade! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

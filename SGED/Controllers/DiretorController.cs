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
    public class DiretorController : ApiController
    {
        // GET: api/Diretor
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<DiretorDTO> lista = new Diretor().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Diretor/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Diretor().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Diretor
        [HttpPost]
        public Boolean Salvar(DiretorDTO Diretor)
        {
            try
            {
                return new Diretor().salvar(Diretor);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Diretor! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Diretor/5
        [HttpPut]
        public Boolean Alterar(DiretorDTO Diretor)
        {
            try
            {
                return new Diretor().salvar(Diretor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Diretor! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Diretor/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Diretor().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Diretor! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

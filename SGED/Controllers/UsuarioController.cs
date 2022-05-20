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
    public class UsuarioController : ApiController
    {
        // GET: api/Usuario
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                IEnumerable<UsuarioDTO> lista = new Usuario().listar(null);
                return Ok(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Usuario/5
        [HttpGet]
        public IHttpActionResult Carregar(int id)
        {
            try
            {
                return Ok(new Usuario().carregar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Usuario
        [HttpPost]
        public Boolean Salvar(UsuarioDTO Usuario)
        {
            try
            {
                return new Usuario().salvar(Usuario);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro na Controller ao salvar Usuario! Erro: " + ex.Message);
                return false;
            }
        }

        // PUT: api/Usuario/5
        [HttpPut]
        public Boolean Alterar(UsuarioDTO Usuario)
        {
            try
            {
                return new Usuario().salvar(Usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao alterar Usuario! Erro: " + ex.Message);
                return false;
            }
        }

        // DELETE: api/Usuario/5
        [HttpDelete]
        public Boolean Excluir(int id)
        {
            try
            {
                return new Usuario().excluir(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na Controller ao excluir Usuario! Erro: " + ex.Message);
                return false;
            }

        }
    }
}

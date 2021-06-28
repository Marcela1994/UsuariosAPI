using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UsuariosApi.Controllers;
using UsuariosApi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsuariosApi.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosApi : ControllerBase
    {
        private IConfiguration configuration;

        public UsuariosApi(IConfiguration iConfig)
        {
            this.configuration = iConfig;
        }
        // GET: api/<UsuariosApi>
        [HttpGet]
        public ActionResult Get()
        {
            UsuariosController usuarioController = new UsuariosController(configuration);
            List<PerfilUsuario> listadoUsuarios = usuarioController.consultarUsuarios();
            return Ok(listadoUsuarios);
        }

        // GET api/<UsuariosApi>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            UsuariosController usuarioController = new UsuariosController(configuration);
            List<PerfilUsuario> listadoUsuario = usuarioController.consultarUsuarioPorID(id);
            return Ok(listadoUsuario);
        }

        // POST api/<UsuariosApi>
        [HttpPost]
        public ActionResult Post([FromBody] Usuarios usuario)
        {
            UsuariosController usuarioController = new UsuariosController(configuration);
            return Ok(usuarioController.insertarUsuario(usuario));
        }

        // PUT api/<UsuariosApi>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Usuarios usuario)
        {
            UsuariosController usuarioController = new UsuariosController(configuration);
            return Ok(usuarioController.modificarUsuario(id, usuario));
        }

        // DELETE api/<UsuariosApi>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            UsuariosController usuarioController = new UsuariosController(configuration);
            return Ok(usuarioController.eliminarUsuario(id));
        }
    }
}

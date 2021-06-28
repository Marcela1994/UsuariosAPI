using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.DAO;
using UsuariosApi.Entities;

namespace UsuariosApi.Controllers
{
    public class UsuariosController
    {
        public IConfiguration iConfig;
        public UsuariosController(IConfiguration iConfig)
        {
            this.iConfig = iConfig;
        }
        public List<PerfilUsuario> consultarUsuarios() {

            UsuariosDAO usuarioDao = new UsuariosDAO(iConfig);
            List<PerfilUsuario> usuariosListado = new List<PerfilUsuario>();
            usuariosListado = usuarioDao.consultarUsuarios();
            return usuariosListado;
        }

        public Respuesta insertarUsuario(Usuarios usuario) {

            UsuariosDAO usuarioDao = new UsuariosDAO(iConfig);
            Respuesta res = new Respuesta();
            int usuarioCreado = usuarioDao.crearUsuario(usuario);
            if (usuarioCreado == 1) {
                res.codigoRespuesta = 1;
                res.mensajeRespuesta = "Usuario creado correctamente";
            }
            else {
                res.codigoRespuesta = 0;
                res.mensajeRespuesta = "No se pudo crear el usuario";
            }
            return res;
        }

        public List<PerfilUsuario> consultarUsuarioPorID(int id)
        {

            UsuariosDAO usuarioDao = new UsuariosDAO(iConfig);
            List<PerfilUsuario> usuariosListado = new List<PerfilUsuario>();
            usuariosListado = usuarioDao.consultarUsuarioPorID(id);
            return usuariosListado;
        }

        public Respuesta modificarUsuario(int id, Usuarios usuario)
        {

            UsuariosDAO usuarioDao = new UsuariosDAO(iConfig);
            Respuesta res = new Respuesta();
            List<PerfilUsuario> usuariosListado = new List<PerfilUsuario>();
            int usuarioModificado = usuarioDao.modificarUsuario(id, usuario);
            if (usuarioModificado == 1)
            {
                res.codigoRespuesta = 1;
                res.mensajeRespuesta = "Usuario modificado correctamente";
            }
            else
            {
                res.codigoRespuesta = 0;
                res.mensajeRespuesta = "No se pudo modificar el usuario";
            }
            return res;
        }

        public Respuesta eliminarUsuario(int id)
        {

            UsuariosDAO usuarioDao = new UsuariosDAO(iConfig);
            Respuesta res = new Respuesta();
            List<PerfilUsuario> usuariosListado = new List<PerfilUsuario>();
            int usuarioModificado = usuarioDao.eliminarUsuario(id);
            if (usuarioModificado == 1)
            {
                res.codigoRespuesta = 1;
                res.mensajeRespuesta = "Usuario eliminado correctamente";
            }
            else
            {
                res.codigoRespuesta = 0;
                res.mensajeRespuesta = "No se pudo eliminado el usuario";
            }
            return res;
        }
    }
}

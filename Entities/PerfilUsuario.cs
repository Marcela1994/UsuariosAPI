using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Entities
{
    public class PerfilUsuario
    {
        public int id { get; set; }
        public string documento { get; set; }
        public string username { get; set; }
        public string nombre { get; set; }
        public string mail { get; set; }
        public Boolean estado { get; set; }
        public Rol rol { get; set; }
        public Dependencias dep { get; set; }
    }
}

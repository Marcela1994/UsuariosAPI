using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Entities
{
    public class Rol
    {
        public int id { get; set; }
        public string descrípcion { get; set; }
        public string siglaRol { get; set; }
        public Boolean estado { get; set; }
    }
}

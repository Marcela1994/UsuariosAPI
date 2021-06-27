using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Entities
{
    public class Dependencias
    {
        public int id { get; set; }
        public string descrípcion { get; set; }
        public string codigo { get; set; }
        public string cargo { get; set; }
        public Boolean estado { get; set; }
    }
}

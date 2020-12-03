using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Operador : Usuario
    {
        public Operador() : base() {
            this.Rol = "Operador";
        }
    }
}

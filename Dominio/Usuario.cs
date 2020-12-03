using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        private string nombreUsuario = "";
        private string password = "";
        private string rol = "";

        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }

        public string Rol { get { return rol; } set { rol = value; } }

        public bool comparePassword(string stringToCompare)
        {
            return password == stringToCompare;
        }

        public string Password { get { return password; } set { password = value; } }

        public Usuario() { }

        public override string ToString()
        {
            return $"nombre: {nombreUsuario}, rol: {rol}.";
        }
    }
}

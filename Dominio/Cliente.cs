using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente : Usuario
    {
        private int cedula = 0;
        private string nombre = "";
        private string apellido = "";


        public int Cedula {
            get { return cedula; }
            set
            {
                cedula = value;
                this.NombreUsuario = value.ToString();
            }
        }

        public string Nombre { get { return nombre; } set { nombre = value; } }

        public string Apellido { get { return apellido; } set { apellido = value; } }

        public Cliente():base()
        {
            this.Rol = "Cliente";
        }

        public override string ToString()
        {
            return $"nombre: {NombreUsuario}, rol: {Rol}, nombre: {nombre}, apellido: {apellido}.";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CompaniaAerea
    {
        private int codigo = 0;
        private string pais = "";

        public CompaniaAerea(int codigo, string pais)
        {
            this.codigo = codigo;
            this.pais = pais;
        }

        public int Codigo
        {
            get { return codigo; }
        }

        public string Pais
        {
            get { return pais; }
            set { pais = value; }
        }

    }
}

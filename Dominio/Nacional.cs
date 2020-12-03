using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Nacional : Excursion
    {
        private bool esDeInteresNacional = false;

        public bool EsDeInteresNacional
        {
            get { return esDeInteresNacional; }
        }

        public Nacional(string descripcion, DateTime fechaComienzo, List<Destino> destinos, int diasTraslado, int stock, bool esDeInteresNacional)
            : base(descripcion, fechaComienzo, destinos, diasTraslado, stock)
        {
            this.esDeInteresNacional = esDeInteresNacional;
        }
    }
}

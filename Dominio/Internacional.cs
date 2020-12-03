using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Internacional : Excursion
    { 
        private CompaniaAerea compania;

        public CompaniaAerea Compania
        {
            get { return compania; }
        }

        public Internacional(string descripcion, DateTime fechaComienzo, List<Destino> destinos, int diasTraslado, int stock, CompaniaAerea compania) 
            : base(descripcion, fechaComienzo, destinos, diasTraslado,stock)
        {
            this.compania = compania;
        }
    }
}

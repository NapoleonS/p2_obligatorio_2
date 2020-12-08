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
        private double comision = 0.1;

        public double Comision
        {
            get { return comision; }
            set { comision = value; }
        }

        public CompaniaAerea Compania
        {
            get { return compania; }
        }

        public Internacional(string descripcion, DateTime fechaComienzo, List<Destino> destinos, int diasTraslado, int stock, CompaniaAerea compania) 
            : base(descripcion, fechaComienzo, destinos, diasTraslado,stock)
        {
            this.compania = compania;
        }

        public new double CalcularCosto(int cantPersonas = 1)
        {
            int result = 0;
            foreach (Destino destino in this.Destinos)
            {
                result += destino.CostoDiario * destino.Dias;
            }

            result = result * cantPersonas;
            result = int.Parse((result + result * comision).ToString());
            
            return result;
        }
    }
}

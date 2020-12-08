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
        private double descuentoTemporadaBaja = 0.1;

        public bool EsDeInteresNacional
        {
            get { return esDeInteresNacional; }
        }

        public double DescuentoTemporadaBaja
        {
            get { return descuentoTemporadaBaja; }
            set { descuentoTemporadaBaja = value; }
        }

        public new double CalcularCosto(int cantPersonas = 1)
        {
            int result = 0;
            foreach (Destino destino in this.Destinos)
            {
                result += destino.CostoDiario * destino.Dias;
            }
            result = result * cantPersonas;

            if(this.FechaComienzo.Month >= 3 && this.FechaComienzo.Month <= 8)
            {
                result = int.Parse((result - result * descuentoTemporadaBaja).ToString());
            }

            return result;
        }

        public Nacional(string descripcion, DateTime fechaComienzo, List<Destino> destinos, int diasTraslado, int stock, bool esDeInteresNacional)
            : base(descripcion, fechaComienzo, destinos, diasTraslado, stock)
        {
            this.esDeInteresNacional = esDeInteresNacional;
        }
    }
}

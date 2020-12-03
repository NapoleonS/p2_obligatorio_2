using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Destino
    {
        private int id = 0;
        private static int ultId = 0;
        private string ciudad = "";
        private string pais = "";
        private int costoDiario = 0;
        private int dias = 0;

        public int Id
        {
            get { return id; }
        }

        public string Pais
        {
            get { return pais; }
        }

        public string Ciudad
        {
            get { return ciudad; }
        }

        public int CostoDiario
        {
            get { return costoDiario; }
        }

        public int Dias
        {
            get { return dias; }
        }

        public Destino(string ciudad, string pais, int dias, int costoDiario)
        {
            this.id = ultId++;
            this.pais = pais;
            this.ciudad = ciudad;
            this.costoDiario = costoDiario;
            this.dias = dias;
        }

        public override string ToString()
        {
            return $"id: {id}, ciudad: {ciudad}, pais: {pais}, costoDiario: {costoDiario}, dias: {dias}.";
        }
    }
}

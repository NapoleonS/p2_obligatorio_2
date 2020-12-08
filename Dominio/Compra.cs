using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        private int id = 0;
        static private int ultId = 0;
        private Excursion excursion;
        private Cliente cliente;
        private int cantMayores = 0;
        private double precio = 0;
        private int cantMenores = 0;
        private DateTime fechaCreacion = new DateTime();

        public Excursion Excursion
        {
            get { return excursion; }
            set {  excursion = value; }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set {  cliente = value; }
        }

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public int CantPasajeros
        {
            get { return cantMayores + cantMenores; }
        }

        public int CantMayores
        {
            get { return cantMayores; }
            set { cantMayores = value; }
        }

        public int CantMenores
        {
            get { return cantMenores; }
            set { cantMenores = value; }
        }

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        
        public int Id
        {
            get { return id; }
        }

        public Compra()
        {
            this.id = ultId++;
            this.fechaCreacion = DateTime.Now;
        }

    }
}

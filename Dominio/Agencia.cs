using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class Agencia
    {
        private static Agencia instance;
        private List<Destino> listaDestinos = new List<Destino>();
        private List<Excursion> listaExcursiones = new List<Excursion>();
        private List<CompaniaAerea> listaCompanias = new List<CompaniaAerea>();
        private double cotizacion = 0;
        private List<Usuario> listaUsuarios = new List<Usuario>();
        private List<Compra> listaCompras = new List<Compra>();
        private double comision = 0.1;

        public Agencia()
        {
            Precarga();
        }

        public double Comision
        {
            get { return comision; }
            set { comision = value; }
        }

        public Usuario BuscarUsuario(string nombreUsuario)
        {
            int i = 0;
            Usuario usuario = null;
            while (usuario == null && i < listaUsuarios.Count)
            {
                if (listaUsuarios[i].NombreUsuario == nombreUsuario)
                {
                    usuario = listaUsuarios[i];
                }
                i++;
            }
            return usuario;
        }

        public List<Excursion> OrdenarExcursionesPorFecha(List<Excursion> lista)
        {
            List<Excursion> result = lista;
            result.Sort((x, y) => DateTime.Compare(y.FechaComienzo, x.FechaComienzo));
            return result;
        }

        public List<Cliente> ListaClientes
        {
            get
            {
                List<Cliente> result = new List<Cliente>();
                foreach (Usuario usuario in listaUsuarios)
                {
                    if (usuario as Cliente != null)
                    {
                        result.Add(usuario as Cliente);
                    }
                }
                return result;
            }
        }

        public List<Cliente> ClientesOrdenados()
        {
            return ListaClientes.OrderBy(s => s.Apellido).ThenBy(s => s.Nombre).ToList();
        }

        public List<Destino> DestinoMasPopular()
        {
            int maxCount = 0;
            List<Destino> result = new List<Destino>();
            foreach (Destino destino in listaDestinos)
            {
            int count = 0;
                foreach (Excursion excursion in ListaExcursiones)
                {
                    if (excursion.Destinos.Contains(destino))
                    {
                        count++;
                    }
                }
                if (count > maxCount)
                {
                    maxCount = count;
                    result.Clear();
                    result.Add(destino);
                }
                else if (count == maxCount)
                {
                    result.Add(destino);
                }
            }
            return result;
        }

        public List<Excursion> BuscarExcursionesPorDestino(Destino destino)
        {
            List<Excursion> result = new List<Excursion>();
            foreach (Excursion excursion in ListaExcursiones)
            {
                if (excursion.Destinos.Contains(destino))
                {
                    result.Add(excursion);
                }
            }
            return result;
        }


        public static Agencia Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Agencia();
                }
                return instance;
            }
        }

        public List<Destino> ListaDestinos
        {
            get { return listaDestinos; }
        }

        public List<Excursion> ListaExcursiones
        {
            get { return listaExcursiones; }
        }

        public List<Usuario> ListaUsuarios
        {
            get { return listaUsuarios; }
        }

        public List<Compra> ListaCompras
        {
            get { return listaCompras; }
        }



        // busca un destino por combinacion ciudad pais
        public Destino BuscarDestinoPorCiudad(string ciudad, string pais)
        { 
            int i = 0;
            Destino destino = null;
            while (destino == null && i < listaDestinos.Count)
            {
                if (listaDestinos[i].Ciudad == ciudad && listaDestinos[i].Pais == pais)
                {
                    destino = listaDestinos[i];
                }
                i++;
            }
            return destino;
        }

        public Destino BuscarDestinoPorId(int id)
        {
            Destino result = null;

            foreach (Destino destino in listaDestinos)
            {
                if (destino.Id == id)
                {
                    result = destino;
                }
            }

            return result;
        }

        public Excursion BuscarExcursion(int id)
        {
            int i = 0;
            Excursion excursion = null;
            while (excursion == null && i < listaExcursiones.Count)
            {
                if (listaExcursiones[i].Id == id)
                {
                    excursion = listaExcursiones[i];
                }
                i++;
            }
            return excursion;
        }

        public Compra BuscarCompra(int id)
        {
            Compra result = null;
            foreach(Compra compra in listaCompras)
            {
                if (compra.Id == id)
                {
                    result = compra;
                }
            }
            return result;
        }

        public List<Compra> ComprasDeCliente(Cliente cliente)
        {
            List<Compra> compras = new List<Compra>();
            foreach (Compra compra in listaCompras)
            {
                if(compra.Cliente == cliente)
                {
                    compras.Add(compra);
                }
            }
            return compras;
        }

        public List<Excursion> ExcursionesADestinoEntreFechas(Destino destino, DateTime fecha1, DateTime fecha2)
        {
            List<Excursion> result = new List<Excursion>();
            foreach (Excursion unaExcursion in listaExcursiones)
            {
                if (DateTime.Compare(fecha1, unaExcursion.FechaComienzo) <= 0 && DateTime.Compare(unaExcursion.FechaComienzo, fecha2) <= 0)
                {
                    result.Add(unaExcursion);
                }
            }
            return result;
        }

        public List<Compra> ComprasEntreFechas(DateTime start, DateTime end)
        {
            List<Compra> result = new List<Compra>();
            foreach (Compra compra in ListaCompras)
            {
                if (compra.FechaCreacion >= start && compra.FechaCreacion < end)
                {
                    result.Add(compra);
                }
            }
            return result;
        }

        public bool AgregarExcursionNacional(string descripcion, DateTime fechaComienzo, List<Destino> destinos, int diasTraslado, int stock,bool esDeInteresNacional)
        {
            bool success = true;
            foreach (var destino in destinos)
            {
                if (destino.Pais != "Uruguay")
                {
                    success = false;
                }
            }
            if (success == true)
            {
            Nacional excursion = new Nacional(descripcion, fechaComienzo, destinos, diasTraslado, stock, esDeInteresNacional);
            listaExcursiones.Add(excursion);
            }
            return success;
        }

        public bool AgregarExcursionInternacional(string descripcion, DateTime fechaComienzo, List<Destino> destinos, int diasTraslado, int stock, CompaniaAerea compania)
        {
            bool success = true;
            Internacional excursion = new Internacional(descripcion, fechaComienzo, destinos, diasTraslado, stock, compania);
            listaExcursiones.Add(excursion);
            return success;
        }

        public bool AgregarCompra(Compra compra)
        {
            bool success = true;
            listaCompras.Add(compra);
            return success;
        }

        public string AgregarCliente(Cliente cliente)
        {
            string response = "ok";
            // TODO: hacer validacion de campos
            
            if (cliente.Cedula <= 999999 || cliente.Cedula >= 1000000000)
            {
                response = "La cedula debe tener entre 7 y 9 digitos";
            }

            if (cliente.Nombre.Length < 2 || cliente.Apellido.Length < 2)
            {
                response = "Tanto el nombre como el apellido deben tener un mínimo de 2 caracteres";
            }

            if (cliente.Password.Length < 6)
            {
                response = "La contraseña debe tener un minimo de 6 caracteres";
            }

            if (response == "ok")
            {
            listaUsuarios.Add(cliente);
            }

            return response;
        }

        public void Precarga()
        {
            PrecargaCotizacion();
            PrecargaDestinos();
            PrecargaExcursiones();
            PrecargarOperadores();
            PrecargaClientes();
            PrecargaCompras();
        }

        private void PrecargaCompras()
        {
            var compra1 = new Compra();
            compra1.CantMayores = 2;
            compra1.Excursion = BuscarExcursion(1200);
            compra1.Precio = compra1.Excursion.CalcularCosto();
            compra1.Cliente = BuscarUsuario("47994721") as Cliente;
            AgregarCompra(compra1);

            var compra2 = new Compra();
            compra2.CantMayores = 2;
            compra2.CantMenores = 3;
            compra2.Excursion = BuscarExcursion(1200);
            compra2.Precio = compra2.Excursion.CalcularCosto();
            compra2.Cliente = BuscarUsuario("47994721") as Cliente;
            AgregarCompra(compra2);
        }

        private void PrecargarOperadores()
        {

            var operador1 = new Operador();
            var operador2 = new Operador();
            operador1.Password = "admin1";
            operador1.NombreUsuario = "admin1";
            operador2.Password = "admin2";
            operador2.NombreUsuario = "admin2";
            listaUsuarios.Add(operador1);
            ListaUsuarios.Add(operador2);
        }

        private void PrecargaClientes()
        {
            var cliente1 = new Cliente();
            cliente1.Password = "Hola1234";
            cliente1.Nombre = "user1";
            cliente1.Apellido = "user1";
            cliente1.Cedula= 47994721;
            AgregarCliente(cliente1);
        }

        public void PrecargaExcursiones()
        {

            List<Destino> listaDestinos1 = new List<Destino>();
            DateTime fecha1 = DateTime.Now.AddDays(14);
            AgregarCompania(1, "Estados Unidos");
            listaDestinos1.Add(BuscarDestinoPorCiudad("Nueva York", "Estados Unidos"));
            AgregarExcursionInternacional("Viaje de placer internacional", fecha1, listaDestinos1, 2, 24, BuscarCompania(1));
            
            DateTime fecha2 = DateTime.Now.AddDays(9);
            List<Destino> listaDestinos2 = new List<Destino>();
            listaDestinos2.Add(BuscarDestinoPorCiudad("Nueva York", "Estados Unidos"));
            listaDestinos2.Add(BuscarDestinoPorCiudad("Londres", "Inglaterra"));
            AgregarExcursionInternacional("Viaje de placer internacional", fecha2, listaDestinos2, 4, 24, BuscarCompania(1));

            DateTime fecha3 = DateTime.Now.AddDays(11);
            List<Destino> listaDestinos3 = new List<Destino>();
            listaDestinos3.Add(BuscarDestinoPorCiudad("Nueva York", "Estados Unidos"));
            listaDestinos3.Add(BuscarDestinoPorCiudad("Londres", "Inglaterra"));
            listaDestinos3.Add(BuscarDestinoPorCiudad("Roma", "Italia"));
            AgregarExcursionInternacional("Viaje de placer internacional", fecha3, listaDestinos3, 5, 21, BuscarCompania(1));

            DateTime fecha4 = DateTime.Now.AddDays(4);
            AgregarCompania(2, "Emiratos Arabes Unidos");
            List<Destino> listaDestinos4 = new List<Destino>();
            listaDestinos4.Add(BuscarDestinoPorCiudad("Dubai", "Emiratos Arabes Unidos"));
            listaDestinos4.Add(BuscarDestinoPorCiudad("Tokyo", "Japon"));
            AgregarExcursionInternacional("Viaje de placer internacional", fecha4, listaDestinos4, 2, 24, BuscarCompania(2));

            DateTime fecha5 = DateTime.Now.AddDays(32);
            List<Destino> listaDestinos5 = new List<Destino>();
            listaDestinos5.Add(BuscarDestinoPorCiudad("Rocha", "Uruguay"));
            AgregarExcursionNacional("Viaje de placer", fecha5, listaDestinos5, 0, 23, false);

            DateTime fecha6 = DateTime.Now.AddDays(100);
            List<Destino> listaDestinos6 = new List<Destino>();
            listaDestinos6.Add(BuscarDestinoPorCiudad("Rivera", "Uruguay"));
            listaDestinos6.Add(BuscarDestinoPorCiudad("Montevideo", "Uruguay"));
            AgregarExcursionNacional("Viaje de placer", fecha6, listaDestinos6, 1, 23, true);

            DateTime fecha7 = new DateTime(2019, 12, 31);
            List<Destino> listaDestinos7 = new List<Destino>();
            listaDestinos7.Add(BuscarDestinoPorCiudad("Rocha", "Uruguay"));
            listaDestinos7.Add(BuscarDestinoPorCiudad("Punta del Este", "Uruguay"));
            AgregarExcursionNacional("Viaje de placer", fecha7, listaDestinos7, 1, 23, false);

            DateTime fecha8 = DateTime.Now.AddDays(3);
            List<Destino> listaDestinos8 = new List<Destino>();
            listaDestinos8.Add(BuscarDestinoPorCiudad("Rocha", "Uruguay"));
            listaDestinos8.Add(BuscarDestinoPorCiudad("Rivera", "Uruguay"));
            listaDestinos8.Add(BuscarDestinoPorCiudad("Montevideo", "Uruguay"));
            listaDestinos8.Add(BuscarDestinoPorCiudad("Punta del Este", "Uruguay"));
            AgregarExcursionNacional("Viaje de placer", fecha8, listaDestinos8, 2, 14, true);
        }

        public void PrecargaCotizacion()
        {
            ModificarCotizacion(42.59); 
        }

        public double CambioADolares(double pesos)
        {
            return pesos * cotizacion;
        }

        public double CambioAPesos(double dolares)
        {
            return dolares / cotizacion;
        }

        public bool BorrarCompra(int id)
        {
            List<Compra> result = new List<Compra>();
            if (BuscarCompra(id) == null)
            {
                return false;
            }
            foreach (Compra compra in listaCompras)
            {
                if (compra.Id != id)
                {
                    result.Add(compra);
                }
            }
            listaCompras = result;
            return true;
        }

        public bool AgregarDestino(string ciudad, string pais, int dias, int costoDiario)
        {
            bool success = false;
            if (BuscarDestinoPorCiudad(ciudad, pais) == null)
            {
                Destino destino = new Destino(ciudad, pais, dias, costoDiario);
                listaDestinos.Add(destino);
                success = true;
            }
            return success;
        }

        public void PrecargaDestinos()
        {
            AgregarDestino("Nueva York", "Estados Unidos", 7, 50);
            AgregarDestino("Rivera", "Uruguay", 8, 20);
            AgregarDestino("Buenos Aires", "Argentina", 7, 25);
            AgregarDestino("Dubai", "Emiratos Arabes Unidos", 7, 55);
            AgregarDestino("Roma", "Italia", 7, 45);
            AgregarDestino("Rocha", "Uruguay", 7, 20);
            AgregarDestino("Montevideo", "Uruguay", 14, 25);
            AgregarDestino("Londres", "Inglaterra", 6, 45);
            AgregarDestino("Punta del Este", "Uruguay", 8, 40);
            AgregarDestino("Tokyo", "Japon", 7, 50);
        }

        public CompaniaAerea BuscarCompania(int codigo)
        {
            int i = 0;
            CompaniaAerea compania = null;
            while (compania == null && i < listaCompanias.Count)
            {
                if (listaCompanias[i].Codigo == codigo)
                {
                    compania = listaCompanias[i];
                }
                i++;
            }
            return compania;
        }

        public bool AgregarCompania(int codigo, string pais)
        {
            bool success = false;
            if (BuscarCompania(codigo) == null)
            {
                CompaniaAerea compania = new CompaniaAerea(codigo, pais);
                listaCompanias.Add(compania);
                success = true;
            }
            return success;
        }

        public bool ModificarCotizacion(double nuevaCotizacion)
        {
            bool success = false;
            if (nuevaCotizacion > 0)
            {
                cotizacion = nuevaCotizacion;
                success = true;
            }
            return success;
        }

       
    }
}

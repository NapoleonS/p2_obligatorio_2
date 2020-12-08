using Dominio;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace appWeb.Controllers
{
    public class ExcursionController : Controller
    {
        Agencia laA = Agencia.Instance;

        public ActionResult Index(string mensaje)
        {
            ViewBag.Excursiones = laA.ListaExcursiones;
            ViewBag.Mensaje = mensaje;
            return View();
        }

        public ActionResult Compra(int id)
        { 
            if ((String)Session["rol"] == null)
            {
                return Redirect("/Usuario/Login");
            }
            Excursion excursion = laA.BuscarExcursion(id);
            ViewBag.excursion = excursion;
            var costoDolares = excursion.CalcularCosto();
            ViewBag.costoDolares = costoDolares;
            ViewBag.costoPesos = laA.CambioAPesos(costoDolares);
            ViewBag.cantMayores = 0;
            ViewBag.cantMenores = 0;
            ViewBag.costo = excursion.CalcularCosto(ViewBag.cantMayores + ViewBag.cantMenores);
            return View(new Compra());
        }

        [HttpPost]
        public ActionResult Compra(Compra compra, int id)
        {
            if ((String)Session["rol"] == null)
            {
                return Redirect("/Usuario/Login");
            }
            Excursion excursion = laA.BuscarExcursion(id);
            excursion.Stock += compra.CantPasajeros; 
            compra.Excursion = excursion;
            string nombreUsuario = (String)Session["user"]; 
            compra.Cliente = laA.BuscarUsuario(nombreUsuario) as Cliente;
            compra.Precio = compra.Excursion.CalcularCosto(compra.CantPasajeros);

            if (laA.AgregarCompra(compra))
            {
                return RedirectToAction("index", new { mensaje = "Se realizó la compra correctamente." });
            }
            ViewBag.Mensaje = "No se pudo realizar la compra";
            return View(compra);
        }

        public ActionResult Estadisticas(int? idDestino)
        {
            if ((String)Session["rol"] != "Operador")
            {
                return Redirect("/Usuario/Login");
            }

            if (idDestino != null)
            {
                Destino destino = laA.BuscarDestinoPorId(idDestino ?? 0);
                List<Excursion> excursiones = laA.BuscarExcursionesPorDestino(destino);
                ViewBag.Resultados = laA.OrdenarExcursionesPorFecha(excursiones);
                ViewBag.idDestino = idDestino;  
            }

            ViewBag.Destinos = laA.ListaDestinos;
            ViewBag.Populares = laA.DestinoMasPopular();

            return View();
        }

        public ActionResult ListaExcursionesPorDestino(int idDestino)
        {
            if ((String)Session["rol"] != "Operador")
            {
                return Redirect("/Usuario/Login");
            }

            Destino destino = laA.BuscarDestinoPorId(idDestino);
            List<Excursion> excursiones = laA.BuscarExcursionesPorDestino(destino);
            ViewBag.Resultados = laA.OrdenarExcursionesPorFecha(excursiones);
            ViewBag.Destinos = laA.ListaDestinos;
            ViewBag.Populares = laA.DestinoMasPopular();
            

            return View("estadisticas");
        }

    }
}
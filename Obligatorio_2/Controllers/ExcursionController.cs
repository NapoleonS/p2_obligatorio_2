using Dominio;
using System;
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
            ViewBag.cantMayores = 0;
            ViewBag.cantMenores = 0;
            ViewBag.costo = excursion.CalcularCosto(ViewBag.cantMayores + ViewBag.cantMenores);
            return View(new Compra());
            // TODO: preguntar como ordenar la lista ascendente por apellido sin un loop
            // TODO: preguntar como es mejor borrar la compra si con un get o un post o un delete
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


    }
}
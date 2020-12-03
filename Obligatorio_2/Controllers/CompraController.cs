using Dominio;
using System;
using System.Web.Mvc;

namespace appWeb.Controllers
{
    public class CompraController : Controller
    {
        Agencia laA = Agencia.Instance;

        public ActionResult Index(string mensaje)
        {
            if ((String)Session["rol"] != "Operador")
            {
                return Redirect("/Usuario/Login");
            }
            ViewBag.Compras = laA.ListaCompras;
            ViewBag.Mensaje = mensaje;
            return View();
        }

        public ActionResult Compra(int id)
        {
            if ((String)Session["rol"] == null)
            {
                return Redirect("/Usuario/Login");
            }
            var excursion = laA.BuscarExcursion(id);
            ViewBag.excursion = excursion;
            ViewBag.cantMayores = 0;
            ViewBag.cantMenores = 0;
            ViewBag.costo = excursion.CalcularCosto(ViewBag.cantMayores + ViewBag.cantMenores);

            return View(new Compra());
        }





    }
}
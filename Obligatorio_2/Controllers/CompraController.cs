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
        
            ViewBag.Mensaje = mensaje;
            return View();
        }


        [HttpPost]
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
            if ((String)Session["rol"] != "Operador")
            {
                return Redirect("/Usuario/Login");
            }

            ViewBag.Compras = laA.ComprasEntreFechas(startDate, endDate);
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View();
        }

    }
}
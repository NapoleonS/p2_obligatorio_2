using Dominio;
using System;
using System.Web.Mvc;

namespace appWeb.Controllers
{
    public class UsuarioController : Controller
    {
        Agencia unaA = Agencia.Instance;

        public ActionResult Login(string mensaje)
        {
            ViewBag.Error = TempData["error"];
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string nombreUsuario, string password)
        {
            Usuario unU = unaA.BuscarUsuario(nombreUsuario);

            if (unU == null || !unU.comparePassword(password))
            {
                TempData["error"] = "Error al hacer login";
                var lista = unaA.ListaExcursiones;
                return Redirect("/Usuario/Login");
            } 
            Session["rol"] = unU.Rol;
            Session["user"] = unU.NombreUsuario;
            return Redirect("/excursion/Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("/Usuario/Login");
        }

        public ActionResult Register() { return View(new Cliente()); }

        [HttpPost]
        public ActionResult Register(Cliente cliente)
        {
            Usuario unU = unaA.BuscarUsuario(cliente.NombreUsuario);

            if (unU != null)
            {
                ViewBag.Mensaje = "El usuario ya existe.";
            }

            if (unU == null)
            {
                // TODO: cambiar esto a try catch
                string mensaje = unaA.AgregarCliente(cliente);
                if (mensaje == "ok")
                {
                ViewBag.Mensaje = "ok";
                return RedirectToAction("login", new { Mensaje = "ok" });
                }
                else
                {
                    ViewBag.Mensaje = $"Se produjo un error: {mensaje}";
                }

            }
            return View();
        }

        public ActionResult Compras(string mensaje)
        {
            if ((String)Session["rol"] == null)
            {
                return Redirect("/Usuario/Login");
            }

            ViewBag.Mensaje = mensaje;
            Cliente usuario = unaA.BuscarUsuario((String)Session["user"]) as Cliente;
            ViewBag.compras = unaA.ComprasDeCliente(usuario);
            return View();
        }


        public ActionResult CancelarCompra(int id)
        {
            if ((String)Session["rol"] == null)
            {
                return Redirect("/Usuario/Login");
            }

            Cliente usuario = unaA.BuscarUsuario((String)Session["user"]) as Cliente;
            if (unaA.BorrarCompra(id))
            {
            ViewBag.Mensaje = "Se ha cancelado con éxito la compra.";
            }

            ViewBag.compras = unaA.ComprasDeCliente(usuario);

            return View("Compras");
        }

        public ActionResult Clientes()
        {
            if ((String)Session["rol"] != "Operador")
            {
                return Redirect("/Usuario/Login");
            }

            ViewBag.Clientes = unaA.ClientesOrdenados();

            return View();
        }

    }
}

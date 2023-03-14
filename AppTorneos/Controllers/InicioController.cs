using AppTorneos.Extensions;
using AppTorneos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppTorneos.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult MenuInicio()
        {
            User usuario = HttpContext.Session.GetObject<User>("USUARIO");
            if(usuario != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("InicioPagina", "LoginUsuario");
            }
            return View();
        }
    }
}

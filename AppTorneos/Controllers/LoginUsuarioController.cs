using Microsoft.AspNetCore.Mvc;
using AppTorneos.Models;
using System.Text;
using AppTorneos.Repositories;
using AppTorneos.Extensions;
using AppTorneos.Helpers;

namespace AppTorneos.Controllers
{
    public class LoginUsuarioController : Controller
    {
        private RepositoryUsuarios repo;
        private HelperApiBrawl helper;

        public LoginUsuarioController(RepositoryUsuarios repo, HelperApiBrawl helper)
        {
            this.repo = repo;
            this.helper = helper;
        }


        public IActionResult InicioPagina()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InicioPagina(string accion, string nombre, string usuariotag, string email, string contrasenia)
        {
            if (accion == "registro")
            {
                //AQUI HAZ EL REGISTRO
                ViewData["MENSAJE"] = "INIASTE REGISTRO";
                if(await helper.Prueba(usuariotag) == false)
                {
                    ViewData["ERROR"] = "Error no existe el usuario";
                }
                else
                {
                    this.repo.InsertarUsuario(usuariotag, nombre, email, contrasenia);
                }

            }else if (accion == "iniciosesion")
            {
                User usuario = this.repo.LoginUsuarios(email, contrasenia);
                if (usuario != null)
                {
                    //GUARDAR EN SESION
                    ViewData["MENSAJE"] = "INIASTE SESION";
                    HttpContext.Session.SetObject("USUARIO", usuario);
                    //return a vista de menu de inicio
                    return RedirectToAction("MenuInicio", "Inicio");
                }
                else
                {
                    ViewData["MENSAJE"] = "ERROR EN LAS CREDENCIALES";
                }
                //AQUI EL INICIO DE SESION
                
                //SI COINCIDE EL USUARIO, GUARDA EN SESION Y REDIRIGE SIGUIENTE VISTA
                //SI NO DEVUELVELE A LA VISTA CON ERROR
            }
            return View();
        }

        public IActionResult CerrarSession()
        {
            HttpContext.Session.Remove("USUARIO");
            return RedirectToAction("InicioPagina");
        }
    }
}

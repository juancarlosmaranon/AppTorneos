using Microsoft.AspNetCore.Mvc;
using AppTorneos.Models;
using System.Text;
using AppTorneos.Repositories;
using AppTorneos.Extensions;
using AppTorneos.Helpers;
using Newtonsoft.Json.Linq;

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
                if(await helper.TokenApi(usuariotag) == false)
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

        public async Task<IActionResult> PerfilUsuario()
        {
            Perfil perf = new Perfil();

            User usu = HttpContext.Session.GetObject<User>("USUARIO");
            if (usu != null)
            {
                JObject jsonusu = await this.helper.InfoUsuarioXTag(usu.UsuarioTag);


                perf.Tag = jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "tag" && x.Type.ToString() == "String").FirstOrDefault().ToString();

                perf.Nombre = jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "name").FirstOrDefault().ToString();
                perf.Trofeos = int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "trophies").FirstOrDefault().ToString());
                perf.MaximoTr = int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "highestTrophies").FirstOrDefault().ToString());
                perf.VictoriasTotales = int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "3vs3Victories").FirstOrDefault().ToString())
                    + int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "soloVictories").FirstOrDefault().ToString())
                    + int.Parse(jsonusu.Values().AsEnumerable().ToList().Where(x => x.Path == "duoVictories").FirstOrDefault().ToString());

                return View(perf);
            }
            else
            {
                return RedirectToAction("InicioPagina", "LoginUsuario");
            }
            
        }
    }
}

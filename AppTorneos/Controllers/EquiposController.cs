using AppTorneos.Extensions;
using AppTorneos.Models;
using AppTorneos.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace AppTorneos.Controllers
{
    public class EquiposController : Controller
    {
        private RepositoryEquipos repo;
        private RepositoryUsuarios repoUsu;

        public EquiposController(RepositoryEquipos repo, RepositoryUsuarios repo22)
        {
            this.repo = repo;
            this.repoUsu = repo22;
        }

        public IActionResult MisEquipos()
        {
            int idj1 = HttpContext.Session.GetObject<User>("USUARIO").IdUsuario;
            List<Equipo> equipos = this.repo.SelectAllEquipos(idj1);

            bool pendientes = false;

            foreach (Equipo equip in equipos)
            {
                if (equip.Jugador1 != idj1)
                {
                    if (equip.Jugador2 == idj1)
                    {
                        if (equip.ConfirmJug2 == -1)
                        {
                            pendientes = true;
                            break;
                        }

                    }else if(equip.Jugador3 == idj1)
                    {
                        if (equip.ConfirmJug3 == -1)
                        {
                            pendientes = true;
                            break;
                        }
                    }
                }
            }
            ViewData["PENDIENTES"] = pendientes;
            return View(equipos);
        }

        [HttpPost]
        public IActionResult MisEquipos(string nombreEquipo, string jugador2, string jugador3)
        {
            int idj1 = HttpContext.Session.GetObject<User>("USUARIO").IdUsuario;

            int idjug2 = this.repoUsu.FindUsuarioXTag(jugador2);
            int idjug3 = this.repoUsu.FindUsuarioXTag(jugador3);

            if (idjug2 != 0 && idjug3 != 0 && idjug3 != idjug2 && idjug2 != idj1 && idjug3 != idj1)
            {
                this.repo.InsertarEquipo(nombreEquipo, idj1, idjug2, idjug3);
            }
            
            return RedirectToAction("MisEquipos", "Equipos");
        }

        public IActionResult BorraEquipo(int idequipo)
        {
            this.repo.DeleteEquipos(idequipo);
            return RedirectToAction("MisEquipos");
        }

        public IActionResult ConfirmaEquipo(int idequipo, bool confirmacion2, bool confirmacion3)
        {
            this.repo.ConfirmaInvitacion(idequipo, confirmacion2, confirmacion3);
            return RedirectToAction("MisEquipos");
        }
    }
}

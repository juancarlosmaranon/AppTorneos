using AppTorneos.Extensions;
using AppTorneos.Models;
using AppTorneos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AppTorneos.Controllers
{
    public class LigaController : Controller
    {

        private RepositoryLigas repo;
        private RepositoryEquipos repoEqu;

        public LigaController(RepositoryLigas repo, RepositoryEquipos repo2)
        {
            this.repo = repo;
            this.repoEqu = repo2;
        }

        public IActionResult MenuLigas()
        {
            ViewData["EQUIPOSUSER"] = this.repoEqu.SelectAllEquipos(HttpContext.Session.GetObject<User>("USUARIO").IdUsuario);
            return View(this.repo.GetLigas());
        }
        [HttpPost]
        public async Task <IActionResult> MenuLigas(string accion, string nombre, int idequipo, DateTime fechainicio, int numequipos, int idliga) 
        {
            if(accion== "CrearLiga")
            {
                await this.repo.CrearLiga(nombre, HttpContext.Session.GetObject<User>("USUARIO").IdUsuario, idequipo);
                return RedirectToAction("MenuLigas","Liga");

            }else if(accion == "BuscarLiga")
            {
                return View(this.repo.FiltrarLigaNombre(nombre));
            }
            else if (accion == "iniciarLiga")
            {

                await this.repo.EmpezarLiga(idliga,fechainicio);
                return RedirectToAction("MenuLigas", "Liga");
            }

            return View(this.repo.GetLigas());
        }

        
        public IActionResult CrearLigas() 
        {
            return View();
        }

        public IActionResult _MenuAdminLiga(int idliga)
        {
            ViewData["LIGA"] = this.repo.GetLiga(idliga);
            ViewData["EQUIPOS"] = this.repo.GetInfoEquiposLiga(idliga);

            return PartialView("_MenuAdminLiga", this.repo.GetEquiposXLiga(idliga));
        }

        public IActionResult _TBodyLigas(int idequipo)
        {
            ViewData["LIGASAPUNTADO"] = this.repo.GetEquipoLigasApuntadas(idequipo);
            ViewData["EQUIPO"] = this.repoEqu.SelectEquipo(idequipo);

            return PartialView("_TBodyLigas", this.repo.GetLigas());
        }

        public async Task<IActionResult> SolicitudAcceso(bool confirmado, int idinscrito)
        {
            await this.repo.AccionAcceso(confirmado, idinscrito);
            return RedirectToAction("MenuLigas","Liga");
        }

        public async Task<IActionResult> EnvioSolicitud(int idliga,int idequipo)
        {
            await this.repo.SolicitarAcceso(idliga, idequipo);
            return RedirectToAction("MenuLigas", "Liga");
        }
    }
}

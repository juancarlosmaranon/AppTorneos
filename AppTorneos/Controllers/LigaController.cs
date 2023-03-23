using AppTorneos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AppTorneos.Controllers
{
    public class LigaController : Controller
    {

        private RepositoryLigas repo;

        public LigaController(RepositoryLigas repo)
        {
            this.repo = repo;
        }

        public IActionResult MenuLigas()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> MenuLigas(string accion, string nombre) 
        {
            if(accion== "CrearLiga")
            {
                await this.repo.CrearLiga(nombre);
            }

            return View();
        }

        
        
        public IActionResult CrearLigas() 
        {
            return View();
        }
    }
}

using AppTorneos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AppTorneos.Controllers
{
    public class EquiposController : Controller
    {
        private RepositoryEquipos repo;

        public EquiposController(RepositoryEquipos repo)
        {
            this.repo = repo;
        }

        public IActionResult MisEquipos()
        {
            return View();
        }

        public IActionResult DetalleEquipo(int idequipo)
        {
            return View();
        }

        public IActionResult CrearEquipo()
        {
            return View();
        }
    }
}

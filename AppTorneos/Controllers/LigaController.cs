using Microsoft.AspNetCore.Mvc;

namespace AppTorneos.Controllers
{
    public class LigaController : Controller
    {   
        public IActionResult MenuLigas() 
        {
            return View();
        }

        public IActionResult CrearLigas() 
        {
            return View();
        }
    }
}

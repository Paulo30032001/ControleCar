using Microsoft.AspNetCore.Mvc;

namespace ControleCar.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}

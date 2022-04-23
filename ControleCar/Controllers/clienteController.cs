using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace ControleCar.Controllers
{
    public class clienteController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}

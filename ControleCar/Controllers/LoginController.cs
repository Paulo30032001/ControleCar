using Microsoft.AspNetCore.Mvc;
using ControleCar.Services;
using ControleCar.Models;
using System.Threading.Tasks;
using ControleCar.Services.Util;
using Microsoft.AspNetCore.Http;

namespace ControleCar.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService service;

        public LoginController(LoginService service)
        {
            this.service = service;
        }

        public IActionResult index()
        {
            HttpContext.Session.Clear();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> index(login login)
        {
            if(login.email!=null && login.senha != null)
            {
                login.senha = Criptografia.sha1(login.senha);
                var usuario = await service.FindByEmail(login.email);

                if (usuario != null)
                {

                    if (usuario.email==login.email && usuario.senha==login.senha && usuario.ativo)
                    {
                        HttpContext.Session.SetInt32("USR_ID", usuario.id);
                        HttpContext.Session.SetString("USR_EMAIL", usuario.email);
                        HttpContext.Session.SetInt32(("USR_ATIVO"),usuario.ativo?1:0);
                        return RedirectToAction("index", "Home");  

                    }
                   else
                    {
                        ViewData["Message"] = "Email ou senha invalidos";
                        return View();
                    }
                }
               
            }

           
                ViewData["Message"] = "Email ou senha invalidos";
                return View();
            



        }

    }
}

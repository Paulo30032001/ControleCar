using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ControleCar.Services;
using ControleCar.Models;
using System.Diagnostics;
using ControleCar.Services.Util;
using Microsoft.AspNetCore.Http;
namespace ControleCar.Controllers
{
    public class usuariosController : Controller
    {
        private usuarioService service { get; set; }

        public usuariosController(usuarioService service)
        {
            this.service = service;
        }

        public  bool Validar()
        {
            if (HttpContext.Session.GetInt32("USR_ID") == null)
            {
                return false;
            }
            if (!HttpContext.Session.GetString("USR_EMAIL").Equals("paulo.eleison@gmail.com"))
            {
                return false;
            }
            
            return true;

        }


        public async Task<IActionResult> index()
        {
            if (!Validar())
            {
                return RedirectToAction("index", "Login");
            }
            var usuario = await service.FindAllAsync();

            return View(usuario);
        }


        public async Task<IActionResult> create()
        {
            if (!Validar())
            {
                return RedirectToAction("index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            usuario.senha = Criptografia.sha1(usuario.senha);
            await service.InsertAsync(usuario);
            return RedirectToAction(nameof(index));
        }


        public async Task<IActionResult> edit(int? id)
        {
            if (!Validar())
            {
                return RedirectToAction("index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }

            var usuario = await service.FindByIdAsync(id.Value);
            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }
            usuario.senha = null;
            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, usuario usuario)
        {
            if (usuario.senha == null)
            {
                var x = await service.FindByIdAsyncAsNoTracking(usuario.id);
                usuario.senha = x.senha;
            }
            else
            {
                usuario.senha = Criptografia.sha1(usuario.senha);
            }

            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsyncAsNoTracking(id);
                return View(x);
            }
            if (id != usuario.id)
            {
                return RedirectToAction(nameof(Error), new { Message = "o Id forncecido não é valido" });
            }
            try
            {
                await service.UpdateAsync(usuario);
                return RedirectToAction(nameof(index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }

        }





        [HttpPost]
        public async Task<IActionResult> delete(int? id)
        {
            if (id == null)
            {
                return Json(new { ok = false });
            }

            var usuario = await service.FindByIdAsync(id.Value);

            if (usuario == null)
            {
                return Json(new { ok = false });
            }

            try
            {
                await service.RemoveAsync(id.Value);
                return Json(new { ok = true });
            }
            catch (Exception e)
            {
                return Json(new { ok = false, Message = e.Message });
            }

        }


        public async Task<IActionResult> Error(string message)
        {
            if (!Validar())
            {
                return RedirectToAction("index", "Login");
            }
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }



    }
}

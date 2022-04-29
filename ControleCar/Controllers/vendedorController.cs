using Microsoft.AspNetCore.Mvc;
using ControleCar.Models;
using ControleCar.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using ControleCar.Services.Util;
namespace ControleCar.Controllers
{
    public class vendedorController : Controller
    {
        private readonly vendedorService service;

        public vendedorController(vendedorService service)
        {
            this.service = service;
        }




        public async Task<IActionResult> index()
        {
            if (!ValidaSessao.Validar(HttpContext))
            {
                return RedirectToAction("index", "Login");
            }

            var vendedor = await service.FindAllAsync();

            return View(vendedor);
        }


        public async Task<IActionResult> create()
        {
            if (!ValidaSessao.Validar(HttpContext))
            {
                return RedirectToAction("index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await service.InsertAsync(vendedor);
            return RedirectToAction(nameof(index));
        }


        public async Task<IActionResult> edit(int? id)
        {
            if (!ValidaSessao.Validar(HttpContext))
            {
                return RedirectToAction("index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }

            var vendedor = await service.FindByIdAsync(id.Value);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }
            return View(vendedor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, vendedor vendedor)
        {
            if (!ValidaSessao.Validar(HttpContext))
            {
                return RedirectToAction("index", "Login");
            }
            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsync(id);
                return View(x);
            }
            if (id != vendedor.id)
            {
                return RedirectToAction(nameof(Error), new { Message = "o Id forncecido não é valido" });
            }
            try
            {
                await service.UpdateAsync(vendedor);
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

            var vendedor = await service.FindByIdAsync(id.Value);

            if (vendedor == null)
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
            if (!ValidaSessao.Validar(HttpContext))
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

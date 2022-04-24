using Microsoft.AspNetCore.Mvc;
using ControleCar.Models;
using ControleCar.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ControleCar.Controllers
{
    public class pecas_departamentosController : Controller
    {
        private readonly pecas_departamentosService service;

        public pecas_departamentosController(pecas_departamentosService service)
        {
            this.service = service;
        }



        public async Task<IActionResult> index()
        {

            var pecas_departamentos = await service.FindAllAsync();

            return View(pecas_departamentos);
        }


        public async Task<IActionResult> create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(pecas_departamentos pecas_departamentos)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await service.InsertAsync(pecas_departamentos);
            return RedirectToAction(nameof(index));
        }


        public async Task<IActionResult> edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }

            var pecas_departamentos = await service.FindByIdAsync(id.Value);
            if (pecas_departamentos == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }
            return View(pecas_departamentos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, pecas_departamentos pecas_departamentos)
        {
            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsync(id);
                return View(x);
            }
            if (id != pecas_departamentos.id)
            {
                return RedirectToAction(nameof(Error), new { Message = "o Id forncecido não é valido" });
            }
            try
            {
                await service.UpdateAsync(pecas_departamentos);
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

            var pecas_departamentos = await service.FindByIdAsync(id.Value);

            if (pecas_departamentos == null)
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
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }



    }
}

using Microsoft.AspNetCore.Mvc;
using ControleCar.Models;
using ControleCar.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using ControleCar.Models.ViewModels;
namespace ControleCar.Controllers
{
    public class pecasController : Controller
    {
        private readonly pecasService service;

        private readonly pecas_departamentosService departamentos_service;

        public pecasController(pecasService service, pecas_departamentosService departamentos_service)
        {
            this.service = service;
            this.departamentos_service = departamentos_service;
        }

        public async Task<IActionResult> index()
        {

            var pecas = await service.FindAllAsync();

            return View(pecas);
        }


        public async Task<IActionResult> create()
        {
            var departamentos = await departamentos_service.FindAllAsync();

            var ViewModels= new pecasFormViewModel() { departamentos=departamentos };

            return View(ViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(pecas pecas)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await departamentos_service.FindAllAsync();

                var ViewModels = new pecasFormViewModel() { departamentos = departamentos };
                return View(ViewModels);
            }
            await service.InsertAsync(pecas);
            return RedirectToAction(nameof(index));
        }


        public async Task<IActionResult> edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }

            var pecas = await service.FindByIdAsync(id.Value);
            if (pecas == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }

            var departamentos = await departamentos_service.FindAllAsync();

            var ViewModels = new pecasFormViewModel() { departamentos = departamentos,pecas=pecas };

            return View(ViewModels);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, pecas pecas)
        {
            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsync(id);
                var departamentos = await departamentos_service.FindAllAsync();

                var ViewModels = new pecasFormViewModel() { departamentos = departamentos,pecas=x };
                return View(ViewModels);
            }
            if (id != pecas.id)
            {
                return RedirectToAction(nameof(Error), new { Message = "o Id forncecido não é valido" });
            }
            try
            {
                await service.UpdateAsync(pecas);
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

            var pecas = await service.FindByIdAsync(id.Value);

            if (pecas == null)
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

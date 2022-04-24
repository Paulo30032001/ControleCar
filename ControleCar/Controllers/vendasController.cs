using Microsoft.AspNetCore.Mvc;
using ControleCar.Models;
using ControleCar.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using ControleCar.Models.ViewModels;

namespace ControleCar.Controllers
{
    public class vendasController : Controller
    {
        private readonly vendasService service;

        private readonly vendedorService vendedorService;


        private readonly clienteService clienteService;


        public vendasController(vendasService service, vendedorService vendedorService, clienteService clienteService)
        {
            this.service = service;
            this.vendedorService= vendedorService;
            this.clienteService= clienteService;
        }



        public async Task<IActionResult> index()
        {

            var vendas = await service.FindAllAsync();

            return View(vendas);
        }


        public async Task<IActionResult> create()
        {
            var vendedores = await vendedorService.FindAllAsync();
            var clientes = await clienteService.FindAllAsync();
            var ViewModels = new vendasFormViewModel() { vendedores = vendedores, clientes = clientes };


            return View(ViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(vendas vendas)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await service.InsertAsync(vendas);
            return RedirectToAction(nameof(index));
        }


        public async Task<IActionResult> edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }

            var vendas = await service.FindByIdAsync(id.Value);
            if (vendas == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }
            return View(vendas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, vendas vendas)
        {
            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsync(id);
                return View(x);
            }
            if (id != vendas.id)
            {
                return RedirectToAction(nameof(Error), new { Message = "o Id forncecido não é valido" });
            }
            try
            {
                await service.UpdateAsync(vendas);
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

            var vendas = await service.FindByIdAsync(id.Value);

            if (vendas == null)
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

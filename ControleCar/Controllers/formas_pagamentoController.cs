using Microsoft.AspNetCore.Mvc;
using ControleCar.Services;
using ControleCar.Models;
using System.Diagnostics;

namespace ControleCar.Controllers
{
    public class formas_pagamentoController : Controller
    {
        private formas_pagamentoService service { get; set; }

        public formas_pagamentoController(formas_pagamentoService service)
        {
            this.service = service;
        }



        public async Task<IActionResult> index()
        {

            var formas_pagamento = await service.FindAllAsync();

            return View(formas_pagamento);
        }


        public async Task<IActionResult> create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(formas_pagamento formas_pagamento)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await service.InsertAsync(formas_pagamento);
            return RedirectToAction(nameof(index));
        }


        public async Task<IActionResult> edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }

            var formas_pagamento = await service.FindByIdAsync(id.Value);
            if (formas_pagamento == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }
            return View(formas_pagamento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, formas_pagamento formas_pagamento)
        {
            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsync(id);
                return View(x);
            }
            if (id != formas_pagamento.id)
            {
                return RedirectToAction(nameof(Error), new { Message = "o Id forncecido não é valido" });
            }
            try
            {
                await service.UpdateAsync(formas_pagamento);
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

            var formas_pagamento = await service.FindByIdAsync(id.Value);

            if (formas_pagamento == null)
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

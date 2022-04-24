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


        private readonly pecasService pecaService;


        private readonly formas_pagamentoService forma_pagService;

        public vendasController(vendasService service, vendedorService vendedorService, clienteService clienteService, pecasService pecaService, formas_pagamentoService forma_pagService)
        {
            this.service = service;
            this.vendedorService= vendedorService;
            this.clienteService= clienteService;
            this.pecaService= pecaService;
            this.forma_pagService = forma_pagService;
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
            var pecas = await pecaService.FindAllAsync();   
            var formas_pagamento = await forma_pagService.FindAllAsync();   

            var ViewModels = new vendasFormViewModel()
            { vendedores = vendedores,
                clientes = clientes,
                 pecas=pecas,
                 formas_pagamentos=formas_pagamento
            
            };


            return View(ViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(vendas vendas)
        {
            if (!ModelState.IsValid)
            {
                var vendedores = await vendedorService.FindAllAsync();
                var clientes = await clienteService.FindAllAsync();
                var pecas = await pecaService.FindAllAsync();
                var formas_pagamento = await forma_pagService.FindAllAsync();

                var ViewModels = new vendasFormViewModel()
                {
                    vendedores = vendedores,
                    clientes = clientes,
                    pecas = pecas,
                    formas_pagamentos = formas_pagamento

                };
                return View(ViewModels);
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
            var vendedores = await vendedorService.FindAllAsync();
            var clientes = await clienteService.FindAllAsync();
            var pecas = await pecaService.FindAllAsync();
            var formas_pagamento = await forma_pagService.FindAllAsync();

            var ViewModels = new vendasFormViewModel()
            {
                vendedores = vendedores,
                clientes = clientes,
                pecas = pecas,
                formas_pagamentos = formas_pagamento,
                vendas=vendas

            };



            return View(ViewModels);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, vendas vendas)
        {
            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsync(id);
                var vendedores = await vendedorService.FindAllAsync();
                var clientes = await clienteService.FindAllAsync();
                var pecas = await pecaService.FindAllAsync();
                var formas_pagamento = await forma_pagService.FindAllAsync();

                var ViewModels = new vendasFormViewModel()
                {
                    vendedores = vendedores,
                    clientes = clientes,
                    pecas = pecas,
                    formas_pagamentos = formas_pagamento,
                    vendas = x

                };



                return View(ViewModels);
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

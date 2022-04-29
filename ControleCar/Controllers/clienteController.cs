using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ControleCar.Services;
using ControleCar.Models;
using System.Diagnostics;
using ControleCar.Services.Util;
namespace ControleCar.Controllers
{
    public class clienteController : Controller
    {

        private  clienteService service { get; set; }

        public clienteController(clienteService service)
        {
            this.service = service; 
        }


        public async Task<IActionResult> index()
        {
            if(!ValidaSessao.Validar(HttpContext))
            {
                return RedirectToAction("index", "Login");
            }
            var clientes = await service.FindAllAsync();

            return View(clientes);
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
        public async Task<IActionResult> create(cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View();   
            }
           await service.InsertAsync(cliente);
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

            var cliente = await service.FindByIdAsync(id.Value);
            if (cliente == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Pagina não Encontrada" });
            }
            return View(cliente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(int id, cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                var x = await service.FindByIdAsync(id);
                return View(x);
            }
            if (id != cliente.id)
            {
                return RedirectToAction(nameof(Error), new { Message = "o Id forncecido não é valido" });
            }
            try
            {
                await service.UpdateAsync(cliente);
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
                return Json(new {ok=false});
            }

            var cliente = await service.FindByIdAsync(id.Value);

            if (cliente == null)
            {
                return Json(new { ok = false });
            }

            try
            {
                await service.RemoveAsync(id.Value);
                return Json(new {ok=true});
            }
            catch(Exception e)
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

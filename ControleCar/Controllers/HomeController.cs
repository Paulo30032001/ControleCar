using ControleCar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ControleCar.Services;
using ControleCar.Services.Util;
namespace ControleCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly homeService service;


        public HomeController(ILogger<HomeController> logger, homeService service)
        {
            _logger = logger;
            this.service = service;
        }

        public IActionResult Index()
        {
            if (!ValidaSessao.Validar(HttpContext))
            {
                return RedirectToAction("index", "Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> pegar_dados(string? data)
        {
            if (string.IsNullOrEmpty(data))
            {
                data = DateTime.Now.ToString();
            }
            string[] vet = data.Split('-');
            string ano = vet[0];
            string mes = vet[1];
            int ano_int = int.Parse(vet[0]);
            int mes_int = int.Parse(vet[1]);
            DateTime data_inicial = DateTime.Parse("01/" + mes + "/" + ano);
            int ultimo = DateTime.DaysInMonth(ano_int, mes_int);
            DateTime data_final = DateTime.Parse(ultimo + "/" + mes + "/" + ano);
            int ativos = await service.ativos();
            int inativos = await service.inativos();
            int numero_vendas = await service.numero_vendas(data_inicial,data_final);
            decimal total_vendas = await service.total_vendas(data_inicial, data_final);
            home_ajax ajax = new home_ajax() 
            { 
                
                usuarios_ativos = ativos,
                usuarios_inativos=inativos,
                numero_vendas=numero_vendas,
                total_vendas=total_vendas
                 
            
            };

            return Json(ajax);

        }


      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (!ValidaSessao.Validar(HttpContext))
            {
                return RedirectToAction("index", "Login");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
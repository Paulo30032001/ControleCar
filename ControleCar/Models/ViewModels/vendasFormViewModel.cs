using ControleCar.Models;
using System.Collections.Generic;
namespace ControleCar.Models.ViewModels
{
    public class vendasFormViewModel
    {
        public vendas vendas { get; set; }

        public List<vendedor> vendedores { get; set; } = new List<vendedor>() { };
        

        public List<cliente> clientes { get; set; } = new List<cliente>() { };  





    }
}

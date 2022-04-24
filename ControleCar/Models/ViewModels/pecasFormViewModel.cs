using System.Collections.Generic;
using ControleCar.Models;
namespace ControleCar.Models.ViewModels
{
    public class pecasFormViewModel
    {
        public pecas pecas { get; set; }

        public List<pecas_departamentos> departamentos { get; set; } = new List<pecas_departamentos>() { };


    }
}

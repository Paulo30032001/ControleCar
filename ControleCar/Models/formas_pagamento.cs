using System.ComponentModel.DataAnnotations;
namespace ControleCar.Models
{
    public class formas_pagamento
    {
        [Display(Name = "Id")]
        [Key]
        public int id { get; set; }

        [Display(Name = "Forma de Pagamento")]
        public string forma_pag { get; set; }

    }
}

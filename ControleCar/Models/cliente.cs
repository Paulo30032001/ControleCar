using System.ComponentModel.DataAnnotations;
namespace ControleCar.Models
{
    public class cliente
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name ="Nome")]
        public string nome { get; set; }
        [Display(Name = "Cpf/Cnpj")]
        public string cpf_cnpj { get; set; }





    }
}

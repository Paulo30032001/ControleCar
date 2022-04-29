using System.ComponentModel.DataAnnotations;
namespace ControleCar.Models
{
    public class usuario
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name = "E-mail")]
        public string email { get; set; }
        [Display(Name = "Senha")]
        public string? senha { get; set; }
        [Display(Name = "Ativo")]
        public bool ativo { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }

    }
}

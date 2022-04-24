using System.ComponentModel.DataAnnotations;
namespace ControleCar.Models
{
    public class vendedor
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }


    }
}

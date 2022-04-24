using System.ComponentModel.DataAnnotations;
namespace ControleCar.Models
{
    public class pecas_departamentos
    {
        [Key]
        [Display(Name ="Id")]
        public int id { get; set; }
        [Display(Name ="Nome")]
        public string nome { get; set; }

    }
}

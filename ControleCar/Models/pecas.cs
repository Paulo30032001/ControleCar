using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleCar.Models
{
    public class pecas
    {
        [Key]
        [Display(Name ="Id")]
        public int id { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [Display(Name = "Valor")]
        public double valor { get; set; }

        [ForeignKey("id_departamento")]
        [Display(Name = "Departamentos")]
        public pecas_departamentos? pecas_departamentos { get; set; }


        public int id_departamento { get; set; }


    }
}

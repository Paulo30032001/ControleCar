using System.ComponentModel.DataAnnotations;
namespace ControleCar.Models
{
    public class usuario
    {
        [Key]
        public int id { get; set; }

        public string email { get; set; }

        public string senha { get; set; }

        public bool ativo { get; set; }

        public string nome { get; set; }

    }
}

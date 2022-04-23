using System.ComponentModel.DataAnnotations;
namespace ControleCar.Models
{
    public class cliente
    {
        [Key]
        public int id { get; set; }

        public string nome { get; set; }

        public string cpf_cnpj { get; set; }





    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleCar.Models
{
    public class vendas
    {
        [Display(Name ="Id")]
        public int id { get; set; }
        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [Display(Name = "Vendedor")]
        [ForeignKey("id_vendedor")]
        public vendedor? vendedor { get; set; }

        public int id_vendedor { get; set; }

        [ForeignKey("id_cliente")]
        [Display(Name = "Cliente")]
        public cliente? cliente { get; set; }

        public int id_cliente { get; set; }

        [ForeignKey("id_peca")]
        [Display(Name = "Peça")]
        public pecas? pecas { get; set; }

        public int id_peca { get; set; }
        [ForeignKey("id_forma_pag")]
        [Display(Name = "Forma de Pagamento")]
        public formas_pagamento? formas_pagamento { get; set; }

        public int id_forma_pag { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
     
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime? data { get; set; }

        public int quantidade { get; set; }

        public decimal valor { get; set; }




    }
}

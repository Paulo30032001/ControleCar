using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleCar.Models
{
    public class vendas
    {
        public int id { get; set; }

        public string tipo { get; set; }

        [ForeignKey("id_vendedor")]
        public vendedor? vendedor { get; set; }

        public int id_vendedor { get; set; }

        [ForeignKey("id_cliente")]
        public cliente? cliente { get; set; }

        public int id_cliente { get; set; }

        [ForeignKey("id_peca")]

        public pecas? pecas { get; set; }

        public int id_peca { get; set; }
        [ForeignKey("id_forma_pag")]

        public formas_pagamento formas_pagamento { get; set; }

        public int id_forma_pag { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? data { get; set; }




    }
}


namespace GestaoPedidosNotificacao.UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ServicoProdutoDress))]
    public partial class ServicoProduto
    {
    }
    public class ServicoProdutoDress
    {

        [Display(Name = "Nome do Serviço")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Qtd")]
        public Nullable<decimal> Quantidade { get; set; }

        [Display(Name = "Valor")]
        public Nullable<decimal> Valor { get; set; }

        [Display(Name = "Última Modificação")]
        public Nullable<System.DateTime> DataModificacao { get; set; }

        [Display(Name = "É um produto?")]
        public Nullable<bool> IsProduto { get; set; }
        
    }
}

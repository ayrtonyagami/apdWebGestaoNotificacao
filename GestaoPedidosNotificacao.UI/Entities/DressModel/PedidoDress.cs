
namespace GestaoPedidosNotificacao.UI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(PedidoDress))]
    public partial class Pedido
    {
    }
    public class PedidoDress
    {

        [Display(Name = "Finalidade")]
        public string Finalidade { get; set; }

        [Display(Name = "Entidade")]
        public Nullable<int> EntidadeId { get; set; }

        [Display(Name = "Valor")]
        public Nullable<decimal> Valor { get; set; }

        [Display(Name = "Status")]
        public Nullable<int> EstadoId { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Data da Factura")]
        public Nullable<System.DateTime> DataFactura { get; set; }

        [Display(Name = "Data do Pagamento")]
        public Nullable<System.DateTime> DataPagamento { get; set; }


        [Display(Name = "Utilizador")]
        public Nullable<int> UtilizadorId { get; set; }

        [Display(Name = "Finalidade (Serviço)")]
        public Nullable<int> ServicoId { get; set; }

        [Display(Name = "Nº Processo")]
        public string NumProcesso { get; set; }
    
    }
}

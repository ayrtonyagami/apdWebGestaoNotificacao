using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Entities
{
    public partial class Pedido
    {
        [Display(Name = "Tempo de Facturas (dias)")]
        public int TempoFactura { get; set; }

        [Display(Name = "Tempo de Pagamento (dias)")]
        public int TempoPagamento { get; set; }

        [Display(Name = "Finalidade (Serviço)")]
        public int ServicoId { get; set; }


        public void CalcularTempo()
        {
            CalcularTempoFactura();
            CalcularTempoPagamento();
        }

        private void CalcularTempoFactura()
        {
            if (DataFactura == null) TempoFactura = 0;
            else TempoFactura = (int)DateTime.Today.Subtract(DataFactura.Value).TotalDays;
        }
        private void CalcularTempoPagamento()
        {
            if (this.DataPagamento== null) TempoPagamento = 0;
            else this.TempoPagamento = (int)DateTime.Today.Subtract(DataPagamento.Value).TotalDays;
        }
    }
}
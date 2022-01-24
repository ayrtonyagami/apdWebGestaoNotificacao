using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Entities
{
    public partial class Pedido
    {
        public int TempoFactura { get; set; }
        public int TempoPagamento { get; set; }

        private void CalcularTempo()
        {
            CalcularTempoFactura();
            CalcularTempoPagamento();
        }

        private void CalcularTempoFactura()
        {
            if (this.DataFactura == null) return;
            this.TempoFactura = (int)this.DataFactura.Value.Subtract(DateTime.Today).TotalDays;
        }
        private void CalcularTempoPagamento()
        {
            if (this.DataPagamento== null) return;
            this.TempoFactura = (int)this.DataPagamento.Value.Subtract(DateTime.Today).TotalDays;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class PedidosAnualChartModel
    {
        public string Mes { get; set; }
        public int nPedidos { get; set; }

    }
    public class PedidosMensalPagasChartModel
    {
        public string Mes { get; set; }
        public decimal ValorMensal { get; set; }

    }
    public class ListPedidosAnualChartModel
    {
        public List<PedidosAnualChartModel> PedidosAnual { get; set; }
        
        public string GetMeses()
        {
            string result = string.Join(",", this.PedidosAnual.Select(x => "'" + x.Mes + "'"));
            return result;
        }

        public string GetNumPedidos()
        {
            return string.Join(",", this.PedidosAnual.Select(x => x.nPedidos));
        }

    }

    public class ListPedidosMensalPagasChartModel
    {
        public List<PedidosMensalPagasChartModel> PedidosPagosAnual { get; set; }

        public string GetMeses()
        {
            string result = string.Join(",", this.PedidosPagosAnual.Select(x => "'" + x.Mes + "'"));
            return result;
        }

        public string GetValoresMensal()
        {
            return string.Join(",", this.PedidosPagosAnual.Select(x => x.ValorMensal));
        }

    }




}
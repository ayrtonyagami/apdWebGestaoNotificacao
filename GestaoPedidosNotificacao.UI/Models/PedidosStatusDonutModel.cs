using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class PedidosStatusDonutModel
    {
        public string StatusName { get; set; }
        public decimal PercStatus { get; set; }
    }


    public class ListPedidosStatusDonutModel
    {
        public List<PedidosStatusDonutModel> PedidosAnual { get; set; }

        public void CalcularPercentange()
        {

            decimal total = this.PedidosAnual.Sum(x => x.PercStatus);

            this.PedidosAnual.ForEach(x =>
            {
                x.PercStatus = (x.PercStatus * 100) / total;
            });

        }

        public string GetStatus()
        {
            string result = string.Join(",", this.PedidosAnual.Select(x => "'" + x.StatusName + "'"));
            return result;
        }

        public string GetPercStatus()
        {
            return string.Join(",", this.PedidosAnual.Select(x => x.PercStatus));
        }

    }


}